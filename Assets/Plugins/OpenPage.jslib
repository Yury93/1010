
mergeInto(LibraryManager.library, {
   
    CloseFrameGame: function() {  
        console.log('close frame WINDOW')
                  var message = JSON.stringify({type:'MyFunctionCall'});  
        window.parent.postMessage(message, '*');  
          console.log('end close frame WINDOW')
    },
       AddScore: function(score) {  
                console.log('add score')
                  var message = JSON.stringify({type:'AddScore',data:score});  
        window.parent.postMessage(message, '*');  
                 console.log('end add score')
    },
});