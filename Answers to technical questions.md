# Technical Questions

## 1. How long did you spend on the coding test?
I spent approximately 12 hours on the coding test.

## 2. What would you add to your solution if you had more time? If you didn't spend much time on the coding test then use this as an opportunity to explain what you would add.
There are several things that I would add to my solution if I had more time, I would focus more on the overall systems architecture, probably going for a specific architecture that will ensure high availability of the API even in times of disasters say hordes Zombies invade a particular facility. In short, I would pay attention to high availability - make use of an API gateway, load balancing and microservices architecture. 

Also, I tried to run the API through 42Crunch API security to validate against OWASP top 10 web application vulnerabilities. I did but I didn't have enough time to treat all the recommendations. 

Also, I would try to achieve 100% coverage on my unit tests. This is a very REAL-life scary stuff, we can't afford to have faulty API functionality.


## 3. How would you track down a performance issue in production? Have you ever had to do this?

Yes, I have had to track down performance issues in production before. Here is a rough process that I would follow:

1. Identify the symptoms: The first step is to identify the symptoms of the performance issue. This might include things like slow response times, high CPU or memory usage, or error messages.

2. Reproduce the issue: Next, try to reproduce the issue in a test environment or staging environment. This will allow you to test different hypotheses and narrow down the root cause more easily.

3. Collect data: Collect as much data as possible about the issue. This might include log files, error received by users, performance metrics, and any other relevant information.

4. Analyze the data: Look for patterns in the data that might indicate the root cause of the issue. This might involve analyzing log files, looking at performance metrics, or using tools like stack traces or profilers to understand what is happening under the hood.

5. Test hypotheses: Based on the data you have collected and analyzed, formulate hypotheses about what might be causing the issue. Then, test these hypotheses by making changes to the system and seeing if the issue is resolved.

6. Implement a fix: Once you have identified the root cause of the issue, implement a fix and test it in a staging environment to ensure that it resolves the issue.

7. Deploy the fix: If the fix is successful, deploy it to production and monitor the system to ensure that the issue has been resolved.


## 4. How would you improve the APIs that you just created?
Currently, I implemented versioning, conformed my API to OpenAPI standards, and have done some documentation. 
However, here are some of the things I would do to improve the APIS I just created: 

1. Use HTTP caching: HTTP caching can help to improve the performance of the API by allowing clients to cache responses and reuse them without having to make a new request to the server. I also will use caching to prevent journeys to the Database everytime. 

2. Use documentation: Providing clear and comprehensive documentation that can can help developers to understand how to use my API and make it more user-friendly.

3. Use error handling: Proper error handling can help to ensure that the API is reliable and easy to use, even when things go wrong.

4. Use security measures: Implementing security measures, such as authentication and authorization, can help to protect the API and the data it handles.

5. Monitor and log API usage: Monitoring and logging API usage can help me to identify any issues or areas for improvement, as well as understand how the API is being used.

6. Test the API: Thoroughly testing the API - stress and load testing that API can help to ensure that it is reliable and performs well under different conditions.

7. Use a more robust database - I am currently using Sqlite because this is a test. I would like to use a more robust, relational database that can handle large amounts of interrelated data.


