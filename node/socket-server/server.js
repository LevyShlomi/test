const express = require('express');
const apiMock= require('./apimock');
var ConnectionManager = require('./connectionManager');

const app = express();
const port = process.env.PORT || 5000;

var server = require('http').createServer(app)
var io = require('socket.io')(server)
var connMgr = new ConnectionManager();

app.use('/', express.static(`${__dirname}/client/build`));

app.get('/api/hello', (req, res) => {
  const obj =  {status: 7};
  let key = req.query.cId;
  let channel = connMgr.getChannel(key);
  if(channel)
    channel.getSocket().emit("testMessage",obj);
  

  res.send("success");
});
app.get('/api/tree', (req,res)=> {
  res.send(apiMock.getTreeData());
});


io.on('connection', (socket) => {
  connMgr.addSocket(socket);
});

server.listen(port);
//app.listen(port, () => console.log(`Listening on port ${port}`));