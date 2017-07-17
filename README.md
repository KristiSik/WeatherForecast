# Project API documentation
## Default cities

* Get all default cities:

`http://localhost:xxx/api/DefaultCities/Get` (method `GET`)

* Add new default city:

`http://localhost:xxx/api/DefaultCities/Add/{name}` (method `POST`)

* Delete default city:

`http://localhost:xxx/api/DefaultCities/Delete/{name}` (method `DELETE`)

## Weather forecast

* Get weather forecast for city:

`http://localhost:xxx/api/Forecast/{name}/{period}` (method `GET`)

## Requests history

* Get all requests history, ordered by time:

`http://localhost:xxx/api/Requests/Get` (method `GET`)
