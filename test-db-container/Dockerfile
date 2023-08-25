# Use the official SQL Server Docker image as the base
FROM mcr.microsoft.com/mssql/server:2019-latest

# Set environment variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=abcDEF123#
ENV MSSQL_PID=Developer
ENV MSSQL_TCP_PORT=1433

# Set the working directory inside the container
WORKDIR /src

# Copy the initialization SQL script into the container
COPY init.sql ./init.sql

# Start SQL Server
RUN (/opt/mssql/bin/sqlservr ${ACCEPT_EULA} &) | grep -q "Service Broker manager has started" && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P ${SA_PASSWORD} -i ${PWD}/init.sql