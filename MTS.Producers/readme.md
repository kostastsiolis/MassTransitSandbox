# Producers
https://masstransit-project.com/usage/producers.html  
An application or service can produce messages using two different methods.  
A message can be sent or a message can be published.  

# Send
To send a message, the DestinationAddress is used to deliver the message to an endpoint — such as a queue.   
One of the Send method overloads on the ISendEndpoint interface is called, which will then send the message to the transport. 
An ISendEndpoint is obtained from one of the following objects:

- The ConsumeContext of the message being consumed

	This ensures that the correlation headers, message headers, and trace information is propagated to the sent message.

- An ISendEndpointProvider instance

	This may be passed as an argument, but is typically specified on the constructor of an object that is resolved using a dependency injection container.

- The IBus

	The last resort, and should only be used for messages that are being sent by an initiator — a process that is initiating a business process.

https://masstransit-project.com/usage/producers.html#publish  
Messages are published similarly to how messages are sent, but in this case, a single IPublishEndpoint is used.  
The same rules for endpoints apply, the closest instance of the publish endpoint should be used.  
So the ConsumeContext for consumers, and IBus for applications that are published outside of a consumer context. 