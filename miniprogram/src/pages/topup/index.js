Page({
  data: {
    moneyBlocks: [
    { "text": "50元", "money": 50 },
    { "text": "100元", "money": 100 },
    { "text": "200元", "money": 200 },
    { "text": "300元", "money": 300 }],
    "money":50
  },
  clickMoneyBlock: function (event) {
    const me = this;
    console.log(event.target.dataset["money"]);
    if(event.target.dataset["money"]){
      me.setData({ money: event.target.dataset["money"]});
    }
  }
});