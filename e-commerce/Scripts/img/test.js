



$(document).ready(function () {
          

        $("#addpost").click(function () {
            var post_txt = $("#commentname").val();
            if (post_txt == "" || post_txt== undefined)
            {
                alert("please enter post")
                return false;
            }
            $.ajax({
                url: "/Home/supmitComment",
                type: "POST",
                datatype: "json",
                data:{
                    post_txt: post_txt
                },
                success: function (data)
                {
                  $("#commentname").val("");

                },
                error:function()
                {
                    alert("failed added co2mment");

                }
            });

            //function processCommentField()
            //{
            //    //document.getElementById('commentname').value = "";
            //    $("#commentname").val("");
            //}

            $("#ggg").css("display", "none");
            //$("#load").show();
            $.ajax({
                url: "/Home/showallcomment",
                type: "GET",
                datatype: "json",
                success: function (data) {
                    var print = "<table class='table' id='ggg' style='width:100%'>"+
                        "<tr>" +
                            "<th>" + "user" + "</th>" +
                            "<th>" + "post" + "</th>" +
                            "<th>" + "delete" + "</th>" +"</tr>" + "</table>";

                     $.each(data, function (i, cat) {

                         var root = "/uploads/" + cat.userimage;
                         var postId = cat.postId;
                         print += "<tr id='deleterecord" + postId + "'>" + "<td><img src='" + root + "'style='border-radius:160px' width='40px' height='40px' alt='mfee4'/><span style='margin:5px'>" + cat.username + "</span></td>"
                             +"<td>" + cat.post_txt + "</td>" +
                "<td><input type='button' value='delete' onclick='Deletecomment(" + postId + ")'/></td>" +
                     + "</tr>";
                    });           
                     //$("#load").hide();

                     document.getElementById('ggg').innerHTML = print;
                    $("#ggg").css("display", "inline-table");
                },
                error: function () {
                    alert("error");
                }
            });


        });
             //----------------------------
        var userid = $("#userid").val();

        //$("#load").show();
        $.ajax({

            url: "/Home/showallcomment",
            type: "GET",
            datatype: "json",
            success: function (data) {
                //console.log(data);

                $.each(data, function (i, cat) {

                    var root = "/uploads/" + cat.userimage;
                    var postId = cat.postId;
                    var userId = cat.userId

                    $("#ggg").append("<tr id='deleterecord" + postId + "'>" +

                "<td><img src='" + root + "'style='border-radius:160px;margin-right: 6px' width='40px' height='40px' alt='mfee4'/><span style='margin:5px'>" + cat.username + "</span></td>" +
                "<td>" + cat.post_txt + "</td>" +
                "<td><input type='button' value='delete' onclick='Deletecomment("+postId+")' id='dis' /></td>" +

               "</tr>");
                   
                    if (userId != userid) {
                        $('#dis').css("display", "none")
                    }
                });
               
                //$("#load").hide();
            },
            error: function () {
                alert("error");
                //alert(data.length2);
            }
        });


        function change() {
           
            //alert(" added sussuss");

                $('#hyperaddtofav').removeClass('icon-heart-empty');
                $('#hyperaddtofav').addClass('icon-heart');
                var prodid = $("#prodid").val();
                var url = "/Home/Details?productid=" + prodid;
                $("Body").load(url);


        }
        function change2() {
            $('#dislike').removeClass('icon-heart');
            $('#dislike').addClass('icon-heart-empty');
            var prodid = $("#prodid").val();

            var url = "/Home/Details?productid=" + prodid;
            $("Body").load(url);
        }

        $('#hyperaddtofav').click(function () {
            var prodid = $("#prodid").val();

            $.ajax({
                url: "/Home/addfavourite",
                type: "POST",
                datatype: "json",
                succuss: change(),

            });
        });


        $('#dislike').click(function () {
            
            var prodid = $("#prodid").val();
            $.ajax({
                url: "/Home/Deletefav",

                type: "POST",
                datatype: "json",
                data:{prodid:prodid},
                success: change2()
            });
        });


});


             var Deletecomment = function (postId) {
                //var prodid = $("#prodid").val();
                $.ajax({
                    url: "/Home/DeleteComment/" + postId,

                    type: "GET",
                    datatype: "json",
                    data: { postId: postId },
                    success: function (status) {
                       $("#deleterecord" + postId).remove();
                    }

                });
            }



