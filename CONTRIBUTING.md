# Getting Started

The fastest way to get up-and-running is to use [JetBrains Rider](https://www.jetbrains.com/rider/). As this repository comes with project configuration for JetBrains IDEs.
This will provide you with automatic plugin support, `SonarQube` integration, and other project defaults.

Alternatively, any other IDE that supports Aspire can be used by using `ChorePoint.AppHost.csproj` as the startup project.
Failing that, you can install the [Aspire CLI](https://github.com/aspire-framework/aspire-cli) which then the same can be achieved by running `aspire run` in your terminal of choice at the repository root.

## Linting
This repository includes a CI pipeline through GitHub Actions that runs on Pull Request/push to master for .NET and TypeScript code issues. These include `CodeQL` for security analysis, [SonarQube](https://sonarcloud.io/project/overview?id=ChorePoint_ChorePoint) for code quality checks across the whole application, [ESLint](https://eslint.org/), and finally build/test checks. Be aware that `SonarQube` does not lint anything under `ChorePoint.Infrastructure/Migrations`.

## Formatting
All the .NET services are formatted at build-time using the built-in `dotnet format` command, this is verified in the .NET GitHub Action.

# Development

When starting to work on a new feature/bug, an issue should already exist or be created. Once you have selected/opened the issue you wish to develop for, on the right-hand side of the screen, click `Create a branch` underneath the `Development` heading. Then switch to that branch locally and commit using [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/).

Once you have finished development, you must create a Pull Request. This will cause the automation to move the issue to the `Pull request` column in the [Kanban board](https://github.com/orgs/ChorePoint/projects/2/views/1), the complete flow can be visualised below:

`Issue created` &rarr; `Branch created from issue` &rarr; `Manually move issue to "In progress" column` &rarr; `Develop` &rarr; `Pull Request created` &rarr; `Automation moves issue to "Pull request" column` &rarr; `Pull Request merged` &rarr; `Automation closes issue`

If a Pull Request gets reviewed, and they request changes, then the flow changes slightly to:

`Pull Request reviewer requests changes` &rarr; `Automation moves issue to "Changes requested" column` &rarr; `Pull Request approved` &rarr; `Automation moves issue to "Pull request" column` &rarr; `Pull Request merged` &rarr; `Automation closes issue`

## Issue Creation
When creating a new issue, make sure to create a descriptive title and add a description if any further information is required, add any labels that apply to the feature/bug, and set the issue to the correct type.

If this new issue is required to be in the next release, then assign it to the latest `Milestone`. This issue should then be manually moved to the `Ready` column on the [Kanban board](https://github.com/orgs/ChorePoint/projects/2/views/1) as all issues that are assigned to a `Milestone` should be in the `Ready` column so they naturally become the next ones to be picked up.

## Pull Request Creation
When creating a new Pull Request, the title needs to be formatted in the following way:

`#<issue_number>: <short_description>`

Assign yourself, then any further information can be added to the Pull Request description.
