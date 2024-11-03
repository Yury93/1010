mergeInto(LibraryManager.library, {
  Hello: function () {
    console.log("Hello!");
  },

  ShowAdvReward: function () {
    AdController.show().then((result) => {
      myGameInstance.SendMessage('Telegram','ShowAdvCallback');
      console.log("your code -- to reward user");
      console.log("Ad result:", result.message);
    }).catch((result ) => {
        myGameInstance.SendMessage('Telegram','ErrorCallback');
      console.log("no reward -- video skipped");
      console.log("Ad error:", result.message);
    });
  }
});