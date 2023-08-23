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

# Start SQL Server, execute the initialization script, and wait for Service Broker manager to start
RUN (/opt/mssql/bin/sqlservr --accept-eula &) | grep -q "Service Broker manager has started" && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P abcDEF123# -i init.sql
