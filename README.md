# RodrigoRuzaCepTest

**RodrigoRuzaCepTest** is a DDD (Domain Driven Design) Rest API made to handle postal code (CEP) information, such as searching for data by CEP, street name (logradouro), and state (UF).

## Features

- **Search by CEP**  
  Retrieves details associated with a specific postal code (CEP).

- **Search by Street Name**  
  Returns addresses associated with a provided street name.

- **Search by State (UF)**  
  Retrieves postal codes (CEPs) associated with a state abbreviation (UF).

## Technologies Used

- **C#:** The primary language for the service.
- **.NET Core 8.0:** Framework used to develop the application.
- **AutoMapper:** For object mapping.
- **Newtonsoft.Json:** For JSON handling.

## Project Structure

```
src/
├── Controllers/
│   ├── CepController.cs         # Controller exposing the CEP-related APIs.
│
├── Domain/
│   ├── Entities/                # Entities related to CEP and street names.
│   ├── Repositories/            # Interfaces for data access.
│   ├── Service/Interface/       # Interfaces for service logic.
│
├── Shared/
│   ├── HttpHandler/             # Interface and implementation for HTTP communication.
│   ├── Models/                  # Response models and extensions.
│   ├── Service/                 # CepService implementation.
```

## Available Endpoints

### 1. Search by CEP
- **Route:** `GET /cep/{cepCode}`  
- **Parameters:**  
  - `cepCode`: A string representing the postal code.  
- **Success Response:**  
  ```json
  {
    "message": "",
    "data": {
      "id": 18,
      "cepCode": "12345678",
      "logradouro": "Rua Exemplo",
      "complemento": "",
      "bairro": "Bairro Exemplo",
      "localidade": "Cidade",
      "uf": "SP",
      "unidade": null,
      "ibge": 1111111,
      "gia": "2222"
    },
    "statusCode": 200
  }
  ```
- **Error Response:**  
  ```json
  {
    "message": "CEP Inválido, digite novamente.",
    "data": null,
    "statusCode": 400
  }
  ```

### 2. Search by Street Name
- **Route:** `GET /cep/logradouro/{logradouro}`  
- **Parameters:**  
  - `logradouro`: The street name (e.g., avenue, road, etc.).  
- **Success Response:**  
  ```json
  {
    "message": "",
    "data": [
      {
      "message": "",
      "data": {
        "id": 18,
        "cepCode": "12345678",
        "logradouro": "Rua Exemplo",
        "complemento": "",
        "bairro": "Bairro Exemplo",
        "localidade": "Cidade",
        "uf": "SP",
        "unidade": null,
        "ibge": 1111111,
        "gia": "2222"
    },
    "statusCode": 200
  }
    ],
    "statusCode": 200
  }
  ```
- **Error Response:**  
  ```json
  {
    "message": "Logradouro não encontrado.",
    "data": null,
    "statusCode": 204
  }
  ```

### 3. Search by State (UF)
- **Route:** `GET /cep/uf/{uf}`  
- **Parameters:**  
  - `uf`: The state abbreviation (e.g., `SP`, `RJ`).  
- **Success Response:**  
  ```json
  {
    "message": "",
    "data": [
      {
        "id": 18,
        "cepCode": "12345678",
        "logradouro": "Rua Exemplo",
        "complemento": "",
        "bairro": "Bairro Exemplo",
        "localidade": "Cidade",
        "uf": "SP",
        "unidade": null,
        "ibge": 1111111,
        "gia": "2222"
      },
    ],
    "statusCode": 200
  }
  ```
- **Error Response:**  
  ```json
  {
    "message": "SP não é uma UF válida.",
    "data": null,
    "statusCode": 400
  }
  ```

## Prerequisites

- **.NET Core SDK 8.0**
- **Database** MySQL configured for repository access.

## How to Run

1. Clone the repository:  
   ```bash
   git clone https://github.com/your-username/your-repository.git
   ```

2. Restore project dependencies:  
   ```bash
   dotnet restore
   ```

3. Configure the connection strings in `appsettings.json` or `appsettings.Development.json`.

4. Run the application:  
   ```bash
   dotnet run
   ```

## Running Tests

To execute automated tests:  
```bash
dotnet test
```
