# Getting Started

The fastest way to get up-and-running is to use [JetBrains Rider](https://www.jetbrains.com/rider/). As this repository comes with project configuration for JetBrains IDEs.
This will provide you with a run configuration called `ChorePoint Dev`, and other project defaults, which starts the Aspire AppHost and dashboard in the default browser.

Alternatively, any other IDE that supports Aspire can be used by using `ChorePoint.AppHost.csproj` as the startup project.
Failing that, you can install the [Aspire CLI](https://github.com/aspire-framework/aspire-cli) which then the same can be achieved by running `aspire run` in your terminal of choice at the repository root.

## Linting
This repository includes a Continuous Integration pipeline that runs multiple actions on pull request to master for .NET and TypeScript code issues. These include `CodeQL` for security analysis, [SonarQube](https://sonarcloud.io/project/overview?id=ChorePoint_ChorePoint) for code quality checks across the whole aplication, [ESLint](https://eslint.org/), and finally build/test checks. Be aware that `SonarQube` does not lint anything under `ChorePoint.Infrastructure/Migrations`.

## Formatting
All the .NET services are formatted at build-time using [CSharpier](https://csharpier.com/), this is validated by the CI pipeline. Alternatively, running `dotnet tool restore` in a terminal of your choice at the repository root will install the `CSharpier` tool. Formatting can then be run manually by then running `dotnet csharpier format .`, this is safe to run at the repository root as the `.csharpierignore` file stops it from formatting anything unwanted.
