var ConnectionManager  = require('./connectionManager');

//import ConnectionManager from './connectionManager';
module.exports = class ServiceProvider {
    constructor(server){
        this.connections = new ConnectionManager(server);

    }

    getProcessService(){
        let channel = this.connections.getChannelForContract("test-service");
        if(channel)
            return new ProcessService(channel);
        else
            return undefined;
    }

}
class IoService {
    constructor(channel){
        this.channel = channel;
        this.send = this.send.bind(this);
    }
    async send(methdName, args){
        return await this.channel.send(methdName, args);
    }

}

class ProcessService extends IoService {
    constructor(channel){
        super(channel);
        this.getProcesses = this.getProcesses.bind(this);

    }

    async getProcesses(){
        return await this.send("list");
    }
}
//export default ServiceProvider;
//module.exports.ServiceProvider