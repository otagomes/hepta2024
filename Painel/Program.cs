using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using Painel.Data;
using Painel.Models;
using System.Globalization;

namespace Painel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ctxContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("ctxContext") ?? throw new InvalidOperationException("Connection string 'ctxContext' not found.")));

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.MaxConcurrentConnections = 100;
                serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024;
                serverOptions.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                serverOptions.Limits.MinResponseDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(200);
                serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(100);
            })
            .UseKestrel()
            .UseUrls("https://*:2900");
            builder.Services.AddSession();            
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "hepta", Version = "v2" });
            });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "pt-BR" };
                options.SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });

            builder.Services.AddCors(options => {
                options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });
            
            var app = builder.Build();

            var defaultDateCulture = "pt-BR";
            var ci = new CultureInfo(defaultDateCulture);
            ci.NumberFormat.NumberDecimalSeparator = ".";
            ci.NumberFormat.CurrencyDecimalSeparator = ".";

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
                app.UseExceptionHandler("/Home/Error");               
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "hepta");
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();
            
            app.UseSession();

            app.UseAuthentication();
                                    
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();            

        }
        private readonly ctxContext _context;
        static async Task processamentoDados()
        {
            /* 
             * SE FOR PRECISO AUTOMATIZAR PROCESSOS NA APLICAÇÃO, ESSA É UMA FORMA POSSÍVEL
             * VANTAGEM: UMA APLICAÇÃO APENAS RESOLVENDO TODOS OS ASSUNTOS
             * DESVANTAGENS: ONERA O SERVIDOR. É PRECISO MONITORAR O USO PARA ESCALAR QUANDO FOR PRECISO
             * 
            string appsetingsJsonFile = "appsettings.json";
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile(appsetingsJsonFile, optional: false, reloadOnChange: true)
                .Build();
            string connString = configuration.GetConnectionString("ctxContext");            
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            while (true)
            {
                List<mensagens_clientes> listMensagenClientes = new();

                string qyMensagemCliente = "select id, id_produto, nome_emitente,sobrenome_emitente, telefone_emitente, email_emitente," +
                                            "       nome_presenteado, telefone_presenteado, dt_envio, hr_envio, status " +
                                            "  from mensagens_clientes " +
                                            " where status = '_pago_'";
                MySqlCommand cmdMensagens = new MySqlCommand(qyMensagemCliente, conn);
                MySqlDataReader rdrMensagens = cmdMensagens.ExecuteReader();
                while (rdrMensagens.Read())
                {
                    mensagens_clientes mensagemCliente = new();
                    mensagemCliente.id = rdrMensagens.GetInt32("id");
                    mensagemCliente.id_produto = rdrMensagens.GetInt32("id_produto");
                    mensagemCliente.nome_emitente = rdrMensagens.GetString("nome_emitente");
                    mensagemCliente.sobrenome_emitente = rdrMensagens.GetString("sobrenome_emitente");
                    mensagemCliente.telefone_emitente = rdrMensagens.GetString("telefone_emitente");
                    mensagemCliente.email_emitente = rdrMensagens.GetString("email_emitente");
                    mensagemCliente.nome_presenteado = rdrMensagens.GetString("nome_presenteado");
                    mensagemCliente.telefone_presenteado = rdrMensagens.GetString("telefone_presenteado");
                    mensagemCliente.dt_envio = rdrMensagens.GetDateTime("dt_envio");
                    mensagemCliente.hr_envio = TimeSpan.Parse(rdrMensagens.GetString("hr_envio"));
                    mensagemCliente.status = rdrMensagens.GetString("status");
                    listMensagenClientes.Add(mensagemCliente);
                }
                rdrMensagens.Close();
                await rdrMensagens.DisposeAsync();
                await cmdMensagens.DisposeAsync();

                if (listMensagenClientes.Count > 0)
                {
                    foreach (mensagens_clientes mensagemCliente in listMensagenClientes)
                    {
                        DateTime dtEnvio = mensagemCliente.dt_envio;
                        TimeSpan hrEnvio = mensagemCliente.hr_envio;
                        if (dtEnvio.Date == DateTime.Now.Date)
                        {
                            if (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) >= hrEnvio)
                            {
                                string nomePresenteado = mensagemCliente.nome_presenteado;
                                string telefonePresenteado = mensagemCliente.telefone_presenteado;
                                string telefoneEmitente = mensagemCliente.telefone_emitente;
                                string nomeEmitente = mensagemCliente.nome_emitente;
                                string sobrenomeEmitente = mensagemCliente.sobrenome_emitente;
                                string emailEmitente = mensagemCliente.email_emitente;
                                string idMensagem = mensagemCliente.id.ToString();
                                string idProduto = mensagemCliente.id_produto.ToString();

                                string qyDetalhesMensagem = "select produto, audio from produtos where id = " + idProduto;
                                MySqlCommand cmdDetalhesMensagem = new MySqlCommand(qyDetalhesMensagem, conn);
                                MySqlDataReader rdrDetalhesMensagem = cmdDetalhesMensagem.ExecuteReader();
                                string tituloMensagem = "";
                                string arquivoMensagem = "";
                                while (rdrDetalhesMensagem.Read())
                                {
                                    tituloMensagem = rdrDetalhesMensagem.GetString("produto");
                                    arquivoMensagem = rdrDetalhesMensagem.GetString("audio");
                                }

                                // . enviar mensagem para o presenteado - wzap
                                string msg = "Olá " + nomePresenteado + ", " + nomeEmitente + " lhe enviou um Zaptelemensagem:";
                                await Lib.EnviarWhatsapp.EnviarWhatsappCliente(telefonePresenteado, msg);
                                Thread.Sleep(500);
                                await Lib.EnviarWhatsapp.EnviarWhatsappClienteAnexo(telefonePresenteado, msg, arquivoMensagem);

                                // . avisar cliente que mensagem foi enviada - wzap
                                msg = nomeEmitente + ", seu ZapTelemensagem foi enviado para " + nomePresenteado;
                                await Lib.EnviarWhatsapp.EnviarWhatsappCliente(telefoneEmitente, msg);

                                
                                await cmdDetalhesMensagem.DisposeAsync();
                                await rdrDetalhesMensagem.CloseAsync();
                                await rdrDetalhesMensagem.DisposeAsync().ConfigureAwait(false);

                                // . avisar zaptlmsg que mensagem foi enviada - email
                                msg = " Nome Emitente: " + nomeEmitente + " " +
                                    "<br/> " +
                                    "Sobrenome Emitente: " + sobrenomeEmitente + " " +
                                    "<br/> " +
                                    "Telefone Emitente: " + telefoneEmitente + " " +
                                    "<br/> " +
                                    "E-mail Emitente: " + emailEmitente + " " +
                                    "<br/> " +
                                    "Nome Presenteado(a): " + nomePresenteado + " " +
                                    "<br/> " +
                                    "Telefone Presenteado(a): " + telefonePresenteado + "  " +
                                    "<br/> " +
                                    "Data de Envio: " + dtEnvio + " " +
                                    "<br/> " +
                                    "Hora de Envio: " + hrEnvio + " " +
                                    "<br/> " +
                                    "ZapTelemensagem Enviado: " + tituloMensagem;
                                await Lib.Email.EnviarEmail("otagomes@hotmail.com", "ZapTelemensagem Enviado - " + nomeEmitente + " para " + nomePresenteado, msg);

                                // . finalizar mensagem
                                string qyUpdMensagemCliente = "update mensagens_clientes set status = '_enviado_' where id = " + idMensagem;
                                MySqlCommand cmdUpdMensagem = new MySqlCommand(qyUpdMensagemCliente, conn);
                                await cmdUpdMensagem.ExecuteNonQueryAsync();
                                await cmdUpdMensagem.DisposeAsync();

                            }
                        }

                    }
                }

                //await conn.CloseAsync();
                //await conn.DisposeAsync();

                //Console.Write("*");
                Thread.Sleep(1000);
            }
            */
        }
    }
}