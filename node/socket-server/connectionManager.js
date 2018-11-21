//import HashMap from 'hashmap';
var HashMap = require('hashmap');
class Channel{
    constructor(__socket, __mgr){
        this.manager = __mgr;
        this.socket = __socket;
        this.context = undefined;
        this.initSocket = this.initSocket.bind(this);
        this.setContext = this.setContext.bind(this);
        this.getContext = this.getContext.bind(this);
        this.getSocket = this.getSocket.bind(this);
        this.getId = this.getId.bind(this);
        this.onReplyMessage = this.onReplyMessage.bind(this);
        this.createUUID = this.createUUID.bind(this);
        this.send = this.send.bind(this);
        this.index = new HashMap();

        this.eventHandlers = {};
    }
    onMessageReply(msg){
        let dict = this.index;
        if(msg.h.id && dict.has(msg.h.id)){
            let state = dict.get(msg.h.id);
            state.resolve(msg.d);
            dict.remove(msg.h.id);
        }
    }

    async send(methodName, args){
        const msg =  {
            h: {
              action: methodName,
              type: "req",
              id: this.createUUID(),
              reply: methodName
            },
            d: args
        }
        var id = msg.h.id;
        var dict = this.index;
        var promise = new Promise((resolve, reject) => {
            dict.set(id, {
                request : msg,
                resolve : resolve,
                reject : reject
            }); 
            setTimeout(function() {
                if(dict.has(id)){
                    let state = dict.get(id);
                    state.reject('timeout');
                    dict.remove(id);
                }
              }, 60000);
            this.getSocket().emit("svcAction",msg);
        });    
           
        return await promise;
        
    }

    createUUID(){
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
          });
        // return ([1e7]+-1e3+-4e3+-8e3+-1e11).replace(/[018]/g, c =>
        //     (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
        //   )
    }
    initSocket(){
        var me = this;
        this.socket
        .on('register', data => {
            this.setContext(data.d);
            this.manager.addChannel(this);
            console.log("registration request - channel connected.");
            console.log(data);
          })
        .on('svcReply', (data) => {
            me.onMessageReply(data);
            console.log(data);  
        })
        .on('disconnect', (data) => {
            var key = this.getId();
            if(key != 0)
                this.manager.removeChannel(key);
          })
        .on('error', (err) => {
            console.log(err);
        });
    }    
    setContext(context){
        this.context = context;
    }
    getContext(){
        return this.context;
    }
    getSocket(){
        return this.socket;
    }
    getId(){
        return this.context ? this.context.id : 0;
    }
    onReplyMessage(callback){
        this.eventHandlers.svcReply = callback;
    }

}
module.exports = class ConnectionManager {
    constructor(server){
        this.io = require('socket.io')(server);
        this.map = new HashMap();
        this.addChannel = this.addChannel.bind(this);
        this.removeChannel = this.removeChannel.bind(this);
        this.addSocket = this.addSocket.bind(this);
        this.getChannel = this.getChannel.bind(this);
        this.getChannelForContract = this.getChannelForContract.bind(this);
        this.initServer = this.initServer.bind(this);

        this.initServer();
    }

    initServer(){
        this.io
        .on('connection', (socket) => {
            this.addSocket(socket);
          })
          .on('error', (err) => {
              console.log(err);
          });
    }

    addChannel(channel){
        this.map.set(channel.getId(), channel);
    }
    removeChannel(key){
        this.map.delete(key);
    }
    getChannel(key){
        return this.map.get(key);
        
    }
    getChannelForContract(contract){
        //this.map.values.where
        let arr = this.map.values();
        console.log(arr);
        for (var i = 0, len = arr.length; i < len; i++) {
            let c = arr[i];
            if(c.getContext().contract == contract){
                return c;
            }
          }
        return undefined;
    }

    addSocket(socket){
        let c = new Channel(socket, this);
        c.initSocket()
        // var timeout = setTimeout(() =>{

        // },10000);
        
    }
}

//export default ConnectionManager;

