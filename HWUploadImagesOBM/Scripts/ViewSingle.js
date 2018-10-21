$(() => {
   
    setInterval(function () {
        const id = $("#im-id").val();
        const image = {
            id: id
        }
       
            $.get('/home/GetLikes', image, function (likes) {
                $("#likes").val(likes);         
            });
            
       },1000);

    $(".btn-like").on('click', function () {
        console.log("hello");
        const id = $(this).data('image-id');
        const image = {
            id: id
        }
        $.post('/home/AddLike', image);
    });
  
});