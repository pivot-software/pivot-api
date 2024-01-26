#!/bin/bash

# Espere até que o banco de dados esteja pronto (ajuste conforme necessário)
wait-for-it.sh database:5433 -t 60

# Inicie a aplicação .NET Core
dotnet ERP.dll