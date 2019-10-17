$(document).ready(function () {

    google.charts.load('current', { 'packages': ['corechart', 'gauge', 'line', 'controls', 'geochart'] });

    //Login Ajax call for User
    $("#LoginUser").click(function () {
        var userData = $("#Login").serialize();
        $.ajax({
            type: "POST",
            url: "/Login/UserLogin",
            data: userData,
            success: function (value) {
                if (value["success"] == true) {
                    location.reload();
                    $('#LoginPartial').load('#LoginPartial');
                    $("#UserLogin").hide();
                }
                else {
                    alert("*Invalid Username or Password");
                }
            },
            error: function (value) {
                location.reload();
                alert("*Invalid Login Attempt")
                $("#UserLogin").hide();
            }
        });
    });


    //Send Message Ajax Call

    $("#SaveComments").click(function () {
        var visitorData = $("#Comment").serialize();
        $.ajax({
            type: "POST",
            url: "/Contact/VisitorComment",
            data: visitorData,
            success: function (value) {
                if (value["success"] == true) {
                    alert("*Message Sent");
                    $("#UserComment").hide();
                    location.reload();
                }
                else {
                    alert("*Please Fill Details Correctly");
                    $("#UserComment").hide();
                    location.reload();
                }
            },
            error: function (value) {
                alert("*Updation Failed");
                $("#UserComment").hide();
                location.reload();
            }
        });
    });



    //Validation for Unique Email Ajax Call

    $("#Email").focusout(function () {
        var email = $("#Email").val();
        // set API Access Key
        var access_key = '591a56f527aa65a1a95315b4334f57bf';
        var email_address = email;
        var message = $("#EmailNotAvailable");

        // verify email address via AJAX call
        $.ajax({
            url: 'http://apilayer.net/api/check?access_key=' + access_key + '&email=' + email_address,
            dataType: 'jsonp',
            success: function (json) {
                if (json.format_valid == true && json.smtp_check == true) {
                    $.ajax({
                        type: "POST",
                        url: "/Register/EmailAvailability",
                        data: '{Email:"' + email + '"}',
                        contentType: "application/json; charset=utf-8",
                        datatype: "json",
                        success: function (result) {
                            var message = $("#EmailNotAvailable");
                            if (result) {
                                if ($("#Email").val() === "") {
                                    message.html("");
                                }
                                else {
                                    message.html("Email ID is available.");
                                    message.css("color", "green");
                                    message.css("font-weight", "bold")
                                }
                            }
                            else {
                                message.html("Email ID is not available.");
                                message.css("color", "red");
                                message.css("font-weight", "bold")
                            }
                        }

                    });
                }
                else {
                    message.html("You have given an Invliad Email !");
                    message.css("color", "red");
                    message.css("font-weight", "bold")
                }

            }
        });
    });
    $("#MoreSkills").click(function () {
        $("#AddSkills").after('<div class="row margin-top20"><input type="text" name="Skills[]" value="" placeholder="Enter Your Skills" class="form-control" /></div>')
    });
    $("#MoreCompany").click(function () {
        $("#AddCompany").after('<div class="row margin-top20"><input type="text" name="Company[]" value="" placeholder="Enter Company Name" class="form-control" /></div>')
    });

    //Selction of Sub-topic Ajax Call for Fetching Topics have ParentID

    $("#Topic").change(function () {
        $("#SubTopic").empty();
        var topicId = parseInt($("#Topic").val());
        $.ajax({
            type: 'POST',
            url: "/Blog/GetMembers?topicId=" + topicId,
            dataType: 'json',
            data: topicId,
            success: function (result) {
                $("#SubTopic").append('<option value="0">--Select Sub-Topic--</option>');
                $.each(result, function (i, member) {
                    $("#SubTopic").append('<option value="' + member.TopicId + '">' + member.TopicName + '</option>');
                });
            },
            error: function (ex) {
                alert('Failed to retrieve SubTopics.');
            }
        });
    });

    //Create a New sub-topic If not Available

    $("#CreateSubTopic").click(function () {
        var topic = parseInt($("#Topic").val());
        var topicName = $("#Topic").find("option:selected").text();
        if (topic != 0) {
            $("#TopicName").attr("value", topicName);
            $("#TopicName").attr("readonly", "true");
        }
        else {
            $("#TopicName").val("");
            $("#TopicName").removeAttr("readonly");
        }
    });

    ///Create a New Topic By Ajax Call
    $("#CreateTopic").click(function () {
        var userData = $("#TopicForm").serialize();
        $.ajax({
            type: "POST",
            url: "/Blog/AddTopic",
            data: userData,
            success: function (value) {
                if (value["success"] == true) {
                    $("#AddTopic").hide();
                    location.reload();
                }
                else {
                    alert("Invalid Topic");
                }
            },
            error: function (value) {
                alert(" Error in adding topic try again !")
                $("#AddTopic").hide();
                location.reload();
            }
        });
    });

    //Adding tags for a selected topic by Ajax call

    $("#SubTopic").focusout(function () {
        var topicId = parseInt($("#SubTopic").val());
        $.ajax({
            type: 'POST',
            url: "/Blog/FetchTags?topicId=" + topicId,
            dataType: 'json',
            data: topicId,
            success: function (result) {
                $.each(result, function (i, member) {
                    $("#SelectedTag").append('<a href="#" class="badge badge-color" style="margin:10px;">' + member.TagTitle + '</a>');
                    $("#Tag").val(function () {
                        return this.value + member.TagTitle;
                    });
                });
            },
            error: function (ex) {
                alert('Failed to retrieve Tags.' + ex);
            }
        });
    });

    //Showing Login PopUp if the User is not logged in

    $("#CreateBlog").click(function () {
        if ($("#GetEmail").val() != null) {
            $("#MyBlog")[0].click();
        }
        else {
            $("#UserLogin").modal('show');
        }
    });

    //Showing Login PopUp if the User is not logged in

    $("#SubmitComment").click(function () {
        if ($("#GetEmail").val() != null) {
            $("#MyBlogComment")[0].click();
        }
        else {
            alert("Please Login to Continue");
        }
    });

    //Showing Login PopUp if the User is not logged in

    $("#SubmitReply").click(function () {
        if ($("#GetEmail").val() != null) {
            $("#MyCommentReply")[0].click();
        }
        else {
            alert("Please Login to Continue");
        }
    });

    //Adding Tag's by Clicking Add buttoon to add tag's given by the user

    $("#AddTag").click(function () {
        var data = $("#MoreItem").val();
        $("#SelectedTag").append('<a href="#" class="badge badge-color-primary" style="margin:10px;">' + data + '</a>');
        $("#MoreItem").val("");
        $("#Tag").val(function () {
            return this.value + data;
        });
    });

    //Ajax call when post is Liked
    $("#LikedPost").click(function () {
        var data = parseInt($("#BlogIdHidden").val());
        $.ajax({
            type: "POST",
            url: "/Blog/Like?blogId=" + data,
            success: function (result) {
                if (result == true) {
                    var likes = parseInt($("#TotalLike").text()) + 1;
                    $("#TotalLike").text(likes);
                    $("#LikedPost").css("background-color", "rgb(187, 243, 186)");
                }
                else {
                    var likes = parseInt($("#TotalLike").text()) - 1;
                    $("#TotalLike").text(likes);
                    $("#LikedPost").css("background-color", "none");
                }
            }
        });

    });
    //Ajax call when post is Disliked
    $("#SpamedPost").click(function () {
        var data = parseInt($("#BlogIdHidden").val());
        $.ajax({
            type: "POST",
            url: "/Blog/Spam?blogId=" + data,
            success: function (result) {
                if (result == true) {
                    var likes = parseInt($("#TotalSpam").text()) + 1;
                    $("#TotalSpam").text(likes);
                    $("#SpamedPost").css("background-color", "rgb(187, 243, 186)");
                }
                else {
                    var likes = parseInt($("#TotalSpam").text()) - 1;
                    $("#TotalSpam").text(likes);
                    $("#SpamedPost").css("background-color", "none");
                }
            }
        });

    });
    //Ajax call when post is Spamed
    $("#DislikedPost").click(function () {
        var data = parseInt($("#BlogIdHidden").val());
        $.ajax({
            type: "POST",
            url: "/Blog/Dislike?blogId=" + data,
            success: function (result) {
                if (result == true) {
                    var likes = parseInt($("#TotalDislike").text()) + 1;
                    $("#TotalDislike").text(likes);
                    $("#DislikedPost").css("background-color", "rgb(187, 243, 186)");
                }
                else {
                    var likes = parseInt($("#TotalLike").text()) - 1;
                    $("#TotalDislike").text(likes);
                    $("#DislikedPost").css("background-color", "none");
                }
            }
        });

    });


    $('#MyGoogleChart').load(
        $.ajax({
            type: "POST",
            url: "/Author/ChartData",
            datatype: "json",
            success: function (result) {
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {
                    var data = google.visualization.arrayToDataTable([
                      ['Task', 'Blog Status'],
                      ['Blog', result.PublishedPostCount],
                      ['Likes', result.LikeCount],
                      ['Comments', result.CommentCount],
                      ['Dislike', result.DislikeCount],
                      ['Spam', result.SpamCount]
                    ]);
                    var options = {
                        title: 'Blog Manager',
                        //is3D: true,
                        pieHole: 0.3,
                    };
                    var charts = new google.visualization.PieChart($('#GraphId')[0]);
                    charts.draw(data, options);
                }
            }
        }))








    //Quill Creation for Ribbon

    var toolbarOption = [

    [{ 'header': [1, 2, 3, 4, 5, 6, true] }],
    ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
    [{ 'size': ['normal', 'small', 'large', 'huge', true] }],
    ['blockquote', 'code-block'],              // custom button values
    [{ 'list': 'ordered' }, { 'list': 'bullet' }],

    [{ 'script': 'sub' }, { 'script': 'super' }],      // superscript/subscript
    [{ 'indent': '-1' }, { 'indent': '+1' }],          // outdent/indent
    [{ 'direction': 'rtl' }],                         // text direction
    [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme

    [{ 'align': [] }],
    ['link', 'image', 'video', 'formula'],
    [{ 'font': [] }],

    ['clean']                                         // remove formatting button
    ];
    var quill = new Quill('#Editor', {
        modules:
            {
                toolbar: toolbarOption
            },
        theme: 'snow'

    });

    var editor = $('.ql-editor');
    editor[0].innerHTML = $("#BlogEditDescription").val();

    htmlbodyHeightUpdate()
    $(window).resize(function () {
        htmlbodyHeightUpdate()
    });
    $(window).scroll(function () {
        height2 = $('.main').height()
        htmlbodyHeightUpdate()
    });

});


$("#SaveBlog").click(function () {
    var data = $(".ql-editor").html();
    $("#Description").val(data);
});
$("#EditedBlogDocument").click(function () {
    var data = $(".ql-editor").html();
    $("#BlogEditDescription").val(data);
});


function htmlbodyHeightUpdate() {
    var height3 = $(window).height()
    var height1 = $('.nav').height() + 50
    height2 = $('.main').height()
    if (height2 > height3) {
        $('html').height(Math.max(height1, height3, height2) + 10);
        $('body').height(Math.max(height1, height3, height2) + 10);
    }
    else {
        $('html').height(Math.max(height1, height3, height2));
        $('body').height(Math.max(height1, height3, height2));
    }

}
$.getScript('//cdn.jsdelivr.net/isotope/1.5.25/jquery.isotope.min.js', function () {

    /* activate jquery isotope */
    $('#posts').imagesLoaded(function () {
        $('#posts').isotope({
            itemSelector: '.item'
        });
    });

});

function deleteComments(CommentId, CreatedBy) {
    $.ajax({
        type: "GET",
        url: "/Blog/DeleteVisitorComments?commentId=" + CommentId + "&email=" + CreatedBy,
        success: function (value) {
            if (value["success"] == true) {
                location.reload();
            } else {
                alert("Deletion Failed");
            }
        },
        error: function (value) {
            alert("Deletion Failed");
        }
    });
}



