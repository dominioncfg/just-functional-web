# Just functional Web

Just functional web is a reference application for [Just Functional](https://dominioncfg.github.io/just-functional-read-the-docs/).

## Getting Started

For trying out this app you need to download the repo and use dotnet sdk to run it.

### Clone the repository

```bash
git clone https://github.com/dominioncfg/just-functional-web.git
```

### Run the Application

```bash
dotnet run --project ./src/JustFunctional.Api --urls=https://localhost:5698/
```

### Use swagger to test the application

If everything went well swagger should be now listening at <https://localhost:5698/swagger>

- From there you can test the validation endpoint.

- For testing the evaluation endpoint, you need to enter the request manually in the browser since swagger doesn’t seem to properly serialize dictionaries in the query string.

### Test endpoints manually

When testing the endpoints manually you need to make sure that the URL are properly encoded, if you use the browser it should doit by default, but that is something to keep in mind you make request using other tools like C#.

#### Validation Endpoint

- Here is an example of calling the validation endpoint for the expression **3+2"**:

<https://localhost:5698/api/v2/math/validate?Expression=3%2B2>

You should get something like this:

```json
{"success":true,"errors":[]}
```

- Here is an example of calling the validation endpoint for the expression **X+2"**:

<https://localhost:5698/api/v2/math/validate?Expression=X%2B2&Variables=X>

You should get something like this:

```json
{"success":true,"errors":[]}
```

#### Evaluation Endpoint

- Here is an example of calling the evaluation endpoint for the expression **3+2"**:

<https://localhost:5698/api/v2/math/evaluate?Expression=3%20%2B2>

You should get something like this:

```json
{"result":5}
```

- Here is an example of calling the evaluation endpoint for the expression **X+2"**:

<https://localhost:5698/api/v2/math/evaluate?Expression=X%20%2B2&Variables[X]=3>

You should get something like this:

```json
{"result":5}
```

## Interesting Files

If you want to know how Just Functional is used you should check this files:

[Controller.cs](/src/JustFunctional.Api/Features/Math/Controller.cs)

[JustFunctionalConfigurationExtensions.cs](/src/JustFunctional.Api/Configuration/JustFunctional/JustFunctionalConfigurationExtensions.cs)
