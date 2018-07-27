$(document).ready(function(){
    console.log( "ready for custom js!" );
    $('[data-toggle="tooltip"]').tooltip(); 
    
    $( "#wally" ).load( "Smack" )
    
  
  //   $.ajax({
  //     method: 'GET',
  //     url:'Smack',
  //     // data: {},
  //     success: function(serverResponse){
  //     console.log(JSON.parse(serverResponse))
  //     $('#wally').text(JSON.parse(serverResponse))
  //     },
  // });
  
  
  // $( "#start" ).click(function(e) {
  //   e.preventDefault()
  //   console.log("clicked hide");
  //   $( "#hide-b" ).hide();
  //   $( "#user_reg" ).show();
  //   $( "#user_login" ).show();
  //   e.preventDefault()
  // });
  
  $( "#login" ).click(function(e) {
    e.preventDefault()
    console.log("clicked hide");
    $( "#hide-b" ).hide();
    $( "#user_reg" ).hide();
    $( "#user_login" ).show();
    $( ".error" ).hide();
    e.preventDefault()
  });
  
  $( "#reg" ).click(function(e) {
    e.preventDefault()
    console.log("clicked hide");
    $( "#hide-b" ).hide();
    $( "#user_reg" ).show();
    $( "#user_login" ).hide();
    $( ".error" ).hide();
    e.preventDefault()
  });
  
  
  function show_login(){
    console.log("working to get login form...")
    $.ajax({
        method: 'GET',
        url: $('#login').attr('href'),
        data:$('#login').serialize(),
        success: function(serverResponse){
        console.log(serverResponse)
        $('#user_login').html(serverResponse)
        }
    })
  };
  
  $('#login').click(function() {
    console.log("selection works")
    show_login(); 
  });    
  
  function show_reg(){
    console.log("working to get registration form...")
    $.ajax({
        method: 'GET',
        url: $('#register').attr('href'),
        data:$('#register').serialize(),
        success: function(serverResponse){
        console.log(serverResponse)
        $('#user_reg').html(serverResponse)
        }
    })
  };
  
  $('#register').click(function() {
    console.log("selection works")
    show_reg(); 
  });  
  
  function sub_reg(){
    console.log("working to submit registration form...")
    $.ajax({
        method: 'POST',
        url: $('#sub_reg').attr('action'),
        data:$('#sub_reg').serialize(),
        success: function(serverResponse){
        console.log(serverResponse)
        $('#user_reg').html(serverResponse)
        }
    })
  };
  
  $('.log_submit').click(function() {
    console.log("selection works")
    sub_reg(); 
  });
  
  function show_smack(){
    console.log("working to get smack talk...")
    $.ajax({
        method: 'POST',
        url: $('#newsmack').attr('action'),
        data:$('#newsmack').serialize(),
        success: function(serverResponse){
        console.log(serverResponse)
        $('#wally').html(serverResponse)
        }
    })
  };
  
  $('#smackbtn').click(function(e) {
    console.log("selection works")
    e.preventDefault()
    show_smack(); 
  }); 
  
  
  // function show_comms(){
  //   console.log("working to get smack comments...")
  //   $.ajax({
  //       method: 'POST',
  //       url: $('#newcomm').attr('action'),
  //       data:$('#newcomm').serialize(),
  //       success: function(serverResponse){
  //       console.log(serverResponse)
  //       $('#wally').html(serverResponse)
  //       }
  //   })
  // };
  
  // $('#commbtn').click(function(e) {
  //   console.log("selection works")
  //   e.preventDefault()
  //   show_comms(); 
  // }); 
  
  
  
  // $('#get_all_button').click(function(){
  //   $.get('/quotes/index_html', function(res) {
  //     $('#quotes').html(res);
  
  // $( ".login" ).click(function(e) {
  //   console.log("clicked show user login");
  
  });
  
  
     
  
  
  // });
  // });
  
  
  // function submitajax_name(){
  //   console.log("working...")
  //   $.ajax({
  //       method: 'POST',
  //       url: $('#ajax_name').attr('action'),
  //       data: $('#ajax_name').serialize(),
  //       success: function(serverResponse){
  //           console.log(serverResponse)
  //           $('#placeholder1').html(serverResponse)
  //       }
  //   })
  // };
  
  // $('input[name=name_includes]').keyup(function() {
  //   submitajax_name(); 
  // });
  
  