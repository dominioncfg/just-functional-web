# Just functional Web

Just functional web is a reference application for [Just Functional](https://dominioncfg.github.io/just-functional-read-the-docs/).

For trying out this app you have two options, you can either use docker or download the repo and use dotnet sdk to run it.

## Run with docker

You can use the following script to create a docker container from the image:

```bash
docker run -dt --name my-evaluator -p 5698:80 josecdom94/just-functional-api:2.0.2
```

If everything went well swagger should be now listening at <http://localhost:5698/swagger>

## Download and run the app

### Clone the repository

```bash
git clone https://github.com/dominioncfg/just-functional-web.git
```

### Run the Application

```bash
dotnet run --project ./src/JustFunctional.Api --urls=http://localhost:5698/
```

If everything went well swagger should be now listening at <http://localhost:5698/swagger>

## Using the application

### Use swagger to test the application

From now on we assume you have an instace of the application running at <http://localhost:5698/> and if you go to <http://localhost:5698/swagger> you sould have a swagger interface to test the application.

- From there you can test the validation endpoint.

- For testing the evaluation endpoint, you need to enter the request manually in the browser since swagger doesn’t seem to properly serialize dictionaries in the query string.

### Test endpoints manually

When testing the endpoints manually you need to make sure that the URL are properly encoded, if you use the browser it should do it by default, but that is something to keep in mind you make request using other tools like C#.

#### Validation Endpoint

- Here is an example of calling the validation endpoint for the expression **3+2**:

<http://localhost:5698/api/v2/math/validate?Expression=3%2B2>

You should get something like this:

```json
{"success":true,"errors":[]}
```

- Here is an example of calling the validation endpoint for the expression **X+2**:

<http://localhost:5698/api/v2/math/validate?Expression=X%2B2&Variables=X>

You should get something like this:

```json
{"success":true,"errors":[]}
```

- Here is an example of calling the validation endpoint for the expression **X+Y**:

```json
{"success":true,"errors":[]}
```

<http://localhost:5698/api/v2/math/validate?Expression=X%2BY&Variables=X&Variables=Y>

#### Evaluation Endpoint

- Here is an example of calling the evaluation endpoint for the expression **3+2**:

<http://localhost:5698/api/v2/math/evaluate?Expression=3%20%2B2>

You should get something like this:

```json
{"result":5}
```

- Here is an example of calling the evaluation endpoint for the expression **X+2**:

<http://localhost:5698/api/v2/math/evaluate?Expression=X%20%2B2&Variables[X]=3>

You should get something like this:

```json
{"result":5}
```

- Here is an example of calling the evaluation endpoint for the expression **X+Y**:

<http://localhost:5698/api/v2/math/evaluate?Expression=X%20%2BY&Variables[X]=3&Variables[Y]=2>

You should get something like this:

```json
{"result":5}
```

Its worth noticing that for both endpoints, the Variables parameter is optional.

## Interesting Files

If you want to know how Just Functional is used you should check this files:

[Controller.cs](/src/JustFunctional.Api/Features/Math/Controller.cs)

[JustFunctionalConfigurationExtensions.cs](/src/JustFunctional.Api/Configuration/JustFunctional/JustFunctionalConfigurationExtensions.cs)

## Build Status

[![Build And Publish](https://github.com/dominioncfg/just-functional-web/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/dominioncfg/just-functional-web/actions/workflows/build.yml)
