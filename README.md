# Programmazione-asincrona-con-async-await
This project contains examples of asynchronous code. It is divided into 4 branches.

Start: .NET Core project in which we have to manage data calls of three entities: Area, Company and Resource.
Synchronous start code.

Step1: Asynchronous code with Task.Run (single instance of the DbContext, generates an exception).

Step2: Asynchronous code revised with Task.Run (created an instance to the DbContext for each method).

Step3: Asynchronous calls using the ToArrayAsync method.

Step4: Three ways to write asynchronous code in comparison
