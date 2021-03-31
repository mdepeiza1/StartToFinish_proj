$(document).ready(function () {
    var API_KEY = $('#API_KEY').val()

    var video = ''

    $("#form").submit(function (event) {
        event.preventDefault()

        var search = $('#search').val()

        videoSearch(API_KEY, search, 10)
    })


    function videoSearch(key, search, maxResults) {
        $("#videos").empty()

        $.get("https://www.googleapis.com/youtube/v3/search?key=" + key + "&type=video&part=snippet"
            + "&maxResults=" + maxResults + "&q=" + search, function (data) {
                console.log(data) 

                data.items.forEach(item => {
                    video = `
                    <iframe width "200" height "200" src="https://www.youtube.com/embed/${item.id.videoId}" frameborder="0" allowfullscreen></iframe> 


                    `
                    $("#videos").append(video)

                });
        })
    }
})