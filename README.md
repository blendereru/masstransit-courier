# masstransit-courier
[From the official documentation:](https://masstransit.io/documentation/concepts/routing-slips)
> Developing applications with a distributed, message-based architecture adds complexity to handling transactions, especially when all steps must either succeed together or fail completely. In traditional applications using an ACID database, transactions are managed through SQL, where partial operations are rolled back if the transaction fails.MassTransit Routing Slips address this challenge by enabling distributed transactions with fault compensation, designed to scale across a network of services. It provides functionality previously handled by database transactions, but adapted for distributed systems.

