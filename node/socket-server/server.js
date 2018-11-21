const express = require('express');
const apiMock= require('./apimock');
//var ConnectionManager = require('./connectionManager').default;
var ServiceProvider  = require('./ServiceProvider');

const app = express();
const port = process.env.PORT || 5000;

var server = require('http').createServer(app)
//var io = require('socket.io')(server)
//var connMgr = new ConnectionManager(server);
var provider = new ServiceProvider(server);

app.use('/', express.static(`${__dirname}/client/build`));

app.get('/api/hello',(async (req, res) => {
  try{

    let svc = provider.getProcessService();
    let processes = await svc.getProcesses();
    res.send(processes);
  } 
  catch(e){

    console.log(e);
  }

  // const obj =  {
  //   h: {
  //     action: "list",
  //     type: "req",
  //     id: "3",
  //     reply: "list"

  //   }
  // };
  // let key = req.query.cId;
  // let channel = connMgr.getChannelForContract("test-service");
  // if(channel)
  //   channel.getSocket().emit("svcAction",obj);
  

  // res.send("success");
}));
app.get('/api/tree', (req,res)=> {
  res.send(apiMock.getTreeData());
});




server.listen(port);
//app.listen(port, () => console.log(`Listening on port ${port}`));