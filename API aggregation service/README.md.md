# API Aggregation Service

## Table of Contents

1. [Introduction](#introduction)
2. [Getting Started](#getting-started)
3. [API Endpoints](#api-endpoints)
4. [Error Handling](#error-handling)
5. [Testing](#testing)
6. [Architecture](#architecture)
7. [Performance and Optimization](#performance-and-optimization)

## Introduction

The API Aggregation Service consolidates data from multiple external APIs and provides a unified endpoint for accessing the aggregated information. This service supports integration with multiple APIs and offers functionalities for filtering, sorting, and performance optimization.

**Integrated APIs:**

- **Weatherbit API:** Provides weather data for various locations. The data includes current weather conditions, forecasts, and more. [Weatherbit API](https://www.weatherbit.io/api/weather-current)
- **News API:** Delivers news articles based on specific keywords. Users can retrieve the latest news and headlines from various sources around the world. [News API](https://newsapi.org/)
- **NASA APOD (Astronomy Picture of the Day) API:** Fetches daily images and information about astronomical objects and phenomena. Users can also retrieve images within a specified date range. [NASA APOD API](https://api.nasa.gov/)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- API keys for the external APIs (WeatherBit API, News API, NASA APIs)
- Docker (for using Redis cache)

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/PapagGeorge/STUDY.PersonalCodingTracker/tree/master/API%20aggregation%20service

2. Restore the Dependencies:
   ```sh
   dotnet restore

3. Install Docker and Start Redis. Ensure Docker is installed and running on your system. Then, start a Redis container using the following command:
   ```sh 
   docker run --name my-redis -d redis
## Configuration

Configure the external API Keys in `appsettings.json`:

```json
{
  "ApiSettings": {
    "WeatherBitApiKey": "Your WeatherBitApiKey API Key",
    "NewsApiKey": "Your NewsApi API Key",
    "NasaApiKey": "Your NasaApiKey API Key",
    "WeatherBitUrl": "http://api.weatherbit.io/v2.0/current",
    "NewsApiBaseUrl": "https://newsapi.org/v2/everything",
    "NasaApiBaseUrl": "https://api.nasa.gov/planetary/apod?"
  }
}
``` 
## API Endpoints

### 1) Aggregated Data Endpoint

- **URL:** `/api/ApiAggregation`
- **Method:** GET
- **Description:** Retrieves aggregated data from external APIs.

**Request Parameters:**

- `newsKeyword` (string, required): Keyword for news search.
- `startDateAstronomyPicture` (string, optional): Start date for astronomy pictures [YYYY-MM-DD]. Must be used with `endDateAstronomyPicture`.
- `endDateAstronomyPicture` (string, optional): End date for astronomy pictures [YYYY-MM-DD]. Must be used with `startDateAstronomyPicture`.
- `sortByAstronomyPicture` (string, optional): Sort criteria for astronomy pictures. Insert `sortByAstronomyPicture=date` to enable sorting by date in the NASA APOD response. Cannot be used without `startDateAstronomyPicture=YYYY-MM-DD` and `endDateAstronomyPicture=YYYY-MM-DD`.
- `ascendingAstronomyPicture` (boolean, optional): Sort order for astronomy pictures. Cannot be used without `sortByAstronomyPicture=date`.
- `ascendingNews` (boolean, optional): Sort order for news.
- `sortByTemperature` (string, optional): Sort weather by temperature. Insert `sortByTemperature=temperature` to sort weather data by temperature.
- `ascendingTemperature` (boolean, optional): Sort order for temperature data. Cannot be used without `sortByTemperature=date`.
- `sortByNews` (string, optional): Sort criteria for news data. Insert `sortByNews=date` to sort news data by date or `sortByNews=author` to sort news data by author.

**Examples:**

- **Basic News Search:** GET /api/ApiAggregation?newsKeyword=technology

- **Astronomy Pictures With Date Range - Date Acsending:**
GET /api/ApiAggregation?newsKeyword=bitcoin&startDateAstronomyPicture=2024-06-10&endDateAstronomyPicture=2024-06-15&sortByAstronomyPicture=date&ascendingAstronomyPicture=true

- **Astronomy Pictures With Date Range - Date Desending:**
GET /api/ApiAggregation?newsKeyword=bitcoin&startDateAstronomyPicture=2024-06-10&endDateAstronomyPicture=2024-06-15&sortByAstronomyPicture=date&ascendingAstronomyPicture=false

- **Sort news by date - Descending:**
GET /api/ApiAggregation?newsKeyword=technology&sortByNews=date&ascendingNews=false

- **Sort weather data by temperature - Ascending:**
GET /api/ApiAggregation?newsKeyword=Bitcoin&sortByTemperature=temperature&ascendingTemperature=true

- **Sort news by author - Descending:**
GET /api/ApiAggregation?newsKeyword=technology&sortByNews=author&ascendingNews=false

**Sample Response:**
GET api/ApiAggregation?newsKeyword=Bitcoin&startDateAstronomyPicture=2024-05-10&endDateAstronomyPicture=2024-05-15&sortByAstronomyPicture=date&ascendingAstronomyPicture=true&ascendingNews=true&sortByTemperature=temperature&ascendingTemperature=true&sortByNews=date

```json
{
  "newsApiResponse": {
    "status": "ok",
    "totalResults": 24688,
    "articles": [
      {
        "author": "Jacob Woodward",
        "title": "EA FC 24 Euro 2024 update: When can you play?",
        "description": "Just as the regular season of football has wound down...",
        "url": "https://readwrite.com/ea-fc-24-euro-2024-update-when-can-you-play/",
        "urlToImage": "https://readwrite.com/wp-content/uploads/2024/06/ea-fc-24-euro-2024.jpg",
        "publishedAt": "2024-06-05T12:37:19Z",
        "content": "Just as the regular season of football has wound down, the Euros has reared its glorious head...",
        "source": {
          "id": null,
          "name": "ReadWrite"
        }
      }
    ]
  },
  "weatherData": [
    {
      "data": [
        {
          "wind_cdir": "NNE",
          "rh": 41,
          "pod": "n",
          "lon": 23.71622,
          "pres": 1005.5,
          "timezone": "Europe/Athens",
          "ob_time": "2024-07-05 22:42",
          "country_code": "GR",
          "clouds": 0,
          "vis": 16,
          "wind_spd": 1.03,
          "gust": 1.03,
          "wind_cdir_full": "north-northeast",
          "app_temp": 24.6,
          "state_code": "ESYE31",
          "ts": 1720219370,
          "h_angle": -90,
          "dewpt": 10.9,
          "weather": {
            "icon": "c01n",
            "code": 800,
            "description": "Clear sky"
          },
          "uv": 0,
          "aqi": 54,
          "station": "AU777",
          "sources": [
            "analysis",
            "AU777",
            "radar",
            "satellite"
          ],
          "wind_dir": 32,
          "elev_angle": -28.87,
          "datetime": "2024-07-05:22",
          "precip": 0,
          "ghi": 0,
          "dni": 0,
          "dhi": 0,
          "solar_rad": 0,
          "city_name": "Athens",
          "sunrise": "03:08",
          "sunset": "17:51",
          "temp": 25,
          "lat": 37.97945,
          "slp": 1013.5
        }
      ]
    }
  ],
  "astronomyPicture": [
    {
      "date": "2024-05-10T00:00:00",
      "explanation": "Relax and watch two black holes merge. Inspired by the first direct detection of gravitational waves in 2015, this simulation plays in slow motion but would take about one third of a second if run in real time….",
      "hdurl": null,
      "media_type": "video",
      "service_version": "v1",
      "title": "Simulation: Two Black Holes Merge",
      "url": "https://www.youtube.com/embed/I_88S8DWbcU?rel=0"
    }
  ]
}
```

### 2)Statistics Endpoint
- **URL:** /api/ApiAggregation/request-statistics
- **Method:** GET
- **Description:** Retrieves request statistics for each external API and groups them in performance buckets. 

**Sample Response:**
GET /api/ApiAggregation/request-statistics
```json
[
  {
    "apiName": "AstronomyApi",
    "totalRequests": 1,
    "fastRequests": 0,
    "averageRequests": 0,
    "slowRequests": 1,
    "fastAverageTime": 0,
    "averageAverageTime": 0,
    "slowAverageTime": 1455
  },
  {
    "apiName": "NewsApi",
    "totalRequests": 3,
    "fastRequests": 0,
    "averageRequests": 0,
    "slowRequests": 3,
    "fastAverageTime": 0,
    "averageAverageTime": 0,
    "slowAverageTime": 453
  },
  {
    "apiName": "WeatherApi",
    "totalRequests": 2,
    "fastRequests": 0,
    "averageRequests": 1,
    "slowRequests": 1,
    "fastAverageTime": 0,
    "averageAverageTime": 189,
    "slowAverageTime": 229
  }
]
```
## Error Handling
The application uses a comprehensive error handling strategy to ensure robustness and reliability when making HTTP calls to external APIs. This strategy revolves around the use of **Polly** for implementing **Retry** and **Circuit Breaker policies**, coupled with traditional try-catch blocks for handling exceptions.

**Polly for Resilience**

**Retry Policy:** I have configured Polly to retry failed HTTP requests using an exponential backoff strategy. This means that if a request fails due to transient issues like network instability or server errors, Polly will automatically retry the request up to 3 times with increasing intervals: initially waiting for 2 seconds, then 4 seconds, and finally 8 seconds.

**Circuit Breaker Policy:** To prevent repeated calls to an API that is consistently failing, I have implemented a circuit breaker policy. This policy monitors the number of consecutive failures. If the threshold is reached (3 consecutive failures), the circuit breaker opens, temporarily preventing further requests to the API for 30 seconds. After this period, it allows a single test request through before fully closing the circuit again if the test is successful.

**Default Object Return:** When the external API fails to respond , the application gracefully returns default responses to the client. The NewsService and the other services are designed to return default responses in case of API failures or timeouts.

**Exception Handling with Try-Catch Blocks**
In addition, the code also utilizes traditional try-catch blocks to handle exceptions that may occur during HTTP requests. Each HTTP request and Service is wrapped in a try-catch block to handle unexpected exceptions gracefully. This ensures that even if an error occurs that is not covered by Polly’s policies (such as network timeouts or unexpected server responses), the application can handle it.


## Testing
The application contains comprehensive unit tests using NUnit to ensure the reliability and functionality of HTTP client calls and corresponding services.

**Unit Test Approach**

**Setup**: Each test initializes necessary mocks (Mock<>) and creates instances of the classes under test.

**HTTP Client Tests:** Validate HTTP requests and responses using mocked **HttpMessageHandler**. Test scenarios include successful API responses, circuit breaker scenarios, and error handling for failed API calls.

**Service Tests:** Test service methods that interact with the HTTP client. Mock the cache (IDistributedCache) to simulate scenarios where data is fetched from the API or from cache. Test cases cover scenarios like fetching data when the cache is empty, retrieving data from cache when available, handling API returning no data, and ensuring sorting functionality.

**Arrange-Act-Assert:** Each test follows the Arrange-Act-Assert pattern to clearly separate the setup, execution, and validation phases. Expected behavior is verified using assertions from NUnit (Assert methods), ensuring that methods return expected results, interact with dependencies as intended, and handle exceptions appropriately.

## Architecture
The structure of the application adheres to the principles of **Clean Architecture**, emphasizing code decoupling and **Dependency Injection** for code decoupling, improved modularity, scalability and testability.

**Layers:**

- **Domain Layer:** Contains core business entities and models that represent the domain concepts of the application. Used in the application as models in order to deserialize the external API responses.

- **Infrastructure Layer:**
Implements concrete implementations of interfaces defined in the application layer. Handles the communication with external resources using **HttpClient** to interact with external APIs.  It also implements our **Fallback mechanism** with **Retry** and **Circuit Breaker policies**.


- **Application Layer:** Contains application-specific logic. It implements the sorting of data and and integrates with Redis Cache to boost performance.

- **Presentation Layer:** The ASP.NET application with one Controller that exposes our 2 endpoints.

- **Unit Tests**: Contains test cases that validate the behavior of components across layers, focusing on functional correctness, error handling, and integration scenarios.

**Dependency Injection** is achieved by using **Microsoft.Extensions.DependencyInjection** in order to register services to the DI container.

## Performance and Optimization
- **Caching Strategy with Redis**: The application attempts to fetch cached responses first before making API calls, optimizing response time for frequently accessed data. If the cache is empty for the specific request it makes an API call and stores the response in the cache memory.

- **Sorting and Processing:** Applies sorting algorithms to organize retrieved data (e.g., news articles) based on specified criteria (e.g., author or date), ensuring that data is presented in a structured and user-friendly manner.

- **Concurrency:** Concurrently initiates tasks to fetch data from the **NewsService**, **WeatherService**, and **AstronomyPictureService** using **Task.WhenAll**.

- **Asynchronous Operations:** Utilizes asynchronous methods to asynchronously await the completion of all tasks.

- **Aggregate Model:** Constructs an AggregateModel object from the results retrieved from each service, ensuring cohesive aggregation of data from disparate sources.
