// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Added later

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build(); //this has an issue with draggable, also may need to be const

//$(function () {
//    $("#sortable1, #sortable2, #sortable3").sortable({
//        connectWith: ".connectedSortable"
//    }).disableSelection();
//});

$(function () {
    var kanbanCol = $('.panel-body');
    kanbanCol.css('max-height', (window.innerHeight - 150) + 'px');

    var kanbanColCount = parseInt(kanbanCol.length);
    $('.container-fluid').css('min-width', (kanbanColCount * 350) + 'px');

    draggableInit();

    $('.panel-heading').click(function () {
        var $panelBody = $(this).parent().children('.panel-body');
        $panelBody.slideToggle();
    });
});

function draggableInit() {
    var sourceId;
    var noteId;

    $('[draggable=true]').bind('dragstart', function (event) {
        sourceId = $(this).parent().attr('id');
        noteId = $(this).attr('id');
        event.originalEvent.dataTransfer.setData("text/plain", event.target.getAttribute('id'));
    });

    $('.panel-body').bind('dragover', function (event) {
        event.preventDefault();
    });

    $('.panel-body').bind('drop', function (event) {
        var children = $(this).children();
        var targetId = children.attr('id');

        //var noteId = $(this).attr('id'); //added later

        if (sourceId != targetId) {

            setTimeout(function () {
                connection.invoke("ChangeColumnsInDatabase", noteId, sourceId, targetId).catch(function (err) {
                    return console.error(err.toString());
                });; //added later
            }, 1000);
            

            var elementId = event.originalEvent.dataTransfer.getData("text/plain");

            $('#processing-modal').modal('toggle'); //before post


            // Post data 
            setTimeout(function () {
                var element = document.getElementById(elementId);
                children.prepend(element);

                $('#processing-modal').modal('toggle'); // after post
            }, 1000);
        }

        event.preventDefault();
        if (sourceId != targetId) {
            setTimeout(function () {
                window.location.reload(true);
            }, 2000);
            //return false;
        }
    });
}

//added for ChatHub
(async () => {
    try {
        await connection.start();
    }
    catch (e) {
        console.error(e.toString());
    }
})();