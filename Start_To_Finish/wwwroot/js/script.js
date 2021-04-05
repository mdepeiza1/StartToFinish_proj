$(document).ready(function () {

    //var API_KEY = $('#API_KEY').val()
    //var itemId = $('#itemId').val()

    $(".submitbut").click(function () {
        var API_KEY = $(this).siblings('#API_KEY').val()
        var itemId = $(this).siblings('#itemId').val()

        event.preventDefault()


        var video = ''

        var search = $('#search' + itemId).val()

        videoSearch(API_KEY, search, 10, itemId)
    });



    $(".submitbut").click(function () {
        var API_KEY = $(this).siblings('#API_KEY').val()
        var itemId = $(this).siblings('#itemId').val()

        event.preventDefault()


        var video = ''

        var search = $('#search' + itemId).val()

        videoSearch(API_KEY, search, 10, itemId)
    });


    //$("#form"+itemId).submit(function (event) {
    //    event.preventDefault()


    //    var video = ''


    //    debugger

    //    var search = $('#search'+itemId).val()

    //    videoSearch(API_KEY, search, 10)
    //})


    function videoSearch(key, search, maxResults, itemId) {
        $("#videos"+itemId).empty()

        $.get("https://www.googleapis.com/youtube/v3/search?key=" + key + "&type=video&part=snippet"
            + "&maxResults=" + maxResults + "&q=" + search, function (data) {
                console.log(data) 

                data.items.forEach(item => {
                    video = `
                    <iframe width "200" height "200" src="https://www.youtube.com/embed/${item.id.videoId}" frameborder="0" allowfullscreen></iframe> 


                    `
                    $("#videos" + itemId).append(video)

                });
        })
    }
})