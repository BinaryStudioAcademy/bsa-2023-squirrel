# Local SQL Server Docker Container for Testing

This repository provides a Docker container with a pre-configured SQL Server instance that contains a sample database schema and initial data.

## Getting Started

1. **Build the Docker Image:**

    Build the Docker image using the provided Dockerfile and initialization script.

    ```bash
    docker build -t test-db-container .
    ```

2. **Run the Docker Container:**

    Start a Docker container from the built image. Replace `YourStrongPassword123` with a strong password for the SQL Server 'sa' user.

    ```bash
    docker run -d -p 1433:1433 --name test-db test-db-container
    ```

4. **Access the SQL Server:**

    Connect to the SQL Server instance using your preferred database management tool (e.g., SQL Server Management Studio or Azure Data Studio).

    - **Server name:** localhost,1433
    - **Authentication:** SQL Server Authentication
        - **Username:** sa
        - **Password:** YourStrongPassword123

5. **Test Database Objects:**

    Once connected to the SQL Server instance, execute SQL queries to test the sample database objects, including tables, stored procedures, functions, and views.

    - Example queries can be found in the 'init.sql'.

6. **Clean Up:**

    When you're finished testing, you can stop and remove the Docker container.

    ```bash
    docker stop test-db
    docker rm test-db
    ```

## Customization

- You can customize the initialization script (`init.sql`) to include additional tables, stored procedures, functions, views, or any other database objects your application requires.
