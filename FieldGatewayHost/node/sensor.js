'use strict';
var fs = require('fs');

/// Sensor module
module.exports = {
    messageBus: null,
    configuration: null,
    someparam: null,
    shouldStop: false,

    create: function (messageBus, configuration) {
        this.messageBus = messageBus;
        this.configuration = configuration;
        this.someparam = this.configuration.someparam;
        //console.log(this.someparam);
        
        setInterval(() => {

            let json = JSON.stringify([
             {
                 'name': 'input',
                 'content': [
                   {
                       'user': 'test1',
                       'temp': 81
                   },
                   {
                       'user': 'test2',
                       'temp': 80
                   },
                   {
                       'user': 'test3',
                       'temp': 82
                   },
                 ]
             }
            ]);
            if (this.shouldStop === false) {
                //console.log("publishing message from sensor");
                this.messageBus.publish({
                    properties: {
                        'source': 'sensor',
                        'name': 'data'
                    },
                    content: new Uint8Array(Buffer.from(json))
                });
            }
        }, 1000);
        return true;

    },

    receive: function(message) {
    },

    destroy: function () {
        this.shouldStop = true;
        //console.log('sensor.destroy');
    }
};
