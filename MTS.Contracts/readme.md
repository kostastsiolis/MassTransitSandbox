https://masstransit-project.com/usage/messages.html#message-names

# Important
MassTransit uses the full type name, including the namespace, for message contracts. 
When creating the same message type in two separate projects, the namespaces must match or the message will not be consumed.

# Tip
It is strongly suggested to use interfaces for message contracts, based on experience over several years with varying levels of developer experience.
MassTransit will create dynamic interface implementations for the messages, ensuring a clean separation of the message contract from the consumer.