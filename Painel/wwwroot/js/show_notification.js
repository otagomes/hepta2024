function showNotification(titulo, mensagem, tipo) {   
    //$.notify({
    //    icon: 'fa fa-bell',
    //    title: "<b>"+titulo+"</b><br/>",
    //    message: mensagem,
    //    target: "_blank"
    //}, {
    //    element: "body",
    //    type: tipo, // primary, danger, warning - é bootstrap, lembra?
    //    allow_dismiss: true,
    //    delay: 15000,
    //    timer: 1000,
    //    animate: {
    //        enter: 'animated fadeInDown',
    //        exit: 'animated fadeOutUp'
    //    },
    //    placement: {
    //        from: "top",
    //        align: "center"
    //    },
    //    z_index: 9999
    //});
}

function showNotificationLong(titulo, mensagem, tipo) {
    $.notify({
        icon: 'fa fa-bell',
        title: "<b>" + titulo + "</b><br/>",
        message: mensagem,
        target: "_blank"
    }, {
        element: "body",
        type: tipo, // primary, danger, warning - é bootstrap, lembra?
        allow_dismiss: true,
        delay: 10000,
        timer: 1000,
        animate: {
            enter: 'animated fadeInDown',
            exit: 'animated fadeOutUp'
        },
        placement: {
            from: "top",
            align: "center"
        },
        z_index: 9999
    });
}