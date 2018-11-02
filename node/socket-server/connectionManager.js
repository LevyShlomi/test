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
    }

    initSocket(){
        this.socket
        .on('register', data => {
            this.setContext(data);
            this.manager.addChannel(this);
            console.log("registration request - channel connected.");
            console.log(data);
          })
        .on('disconnect', (data) => {
            var key = this.getId();
            if(key != 0)
                this.manager.removeChannel(this);
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

}
class ConnectionManager {
    constructor(){
        this.map = new HashMap();
        this.addChannel = this.addChannel.bind(this);
        this.removeChannel = this.removeChannel.bind(this);
        this.addSocket = this.addSocket.bind(this);
        this.getChannel = this.getChannel.bind(this);
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

    addSocket(socket){
        let c = new Channel(socket, this);
        c.initSocket()
        // var timeout = setTimeout(() =>{

        // },10000);
        
    }
}

module.exports = ConnectionManager;
