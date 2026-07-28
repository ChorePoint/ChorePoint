# Getting Started

The fastest way to get up-and-running is to use [JetBrains Rider](https://www.jetbrains.com/rider/). As this repository comes with project configuration for JetBrains IDEs.
This will provide you with a run configuration called `ChorePoint Dev`, and other project defaults, which starts the Aspire AppHost and dashboard in the default browser.

Alternatively, any other IDE that supports Aspire can be used by using `ChorePoint.AppHost.csproj` as the startup project.
Failing that, you can install the [Aspire CLI](https://github.com/aspire-framework/aspire-cli) which then the same can be achieved by running `aspire run` in your terminal of choice at the repository root.

## Linting
This repository includes a CI pipeline through GitHub Actions that runs on pull request/push to master for .NET and TypeScript code issues. These include `CodeQL` for security analysis, [SonarQube](https://sonarcloud.io/project/overview?id=ChorePoint_ChorePoint) for code quality checks across the whole application, [ESLint](https://eslint.org/), and finally build/test checks. Be aware that `SonarQube` does not lint anything under `ChorePoint.Infrastructure/Migrations`.

## Formatting
All the .NET services are formatted at build-time using the built-in `dotnet format` command, this is verified in the .NET GitHub Action.
