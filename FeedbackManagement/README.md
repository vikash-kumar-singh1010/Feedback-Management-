# Feedback Management Module (ASP.NET Core MVC, .NET 8)

Ex-7: MVC layout, partial views, HTML helpers and validation.

## Run
    cd FeedbackManagement
    dotnet run
Open the https://localhost:xxxx URL shown in the console.

## Two flows in one project
| | Flow 1: Classic (/Feedback) | Flow 2: Single page (/Quick) |
|---|---|---|
| Navigation | Index -> Create -> redirect -> Index | One page, no reloads |
| Submit | Full post-back, Post-Redirect-Get | fetch() AJAX |
| Partial views | _FeedbackFields shared by Create/Edit | _Form and _List swapped into the page |
| Validation | Data annotations + custom [NoBadWords], client + server | Same, re-bound after each AJAX response |

## Key files
- Views/Shared/_Layout.cshtml          layout (navbar, RenderBody, RenderSection)
- Views/Shared/_FeedbackFields.cshtml  reusable partial with the HTML helpers
- Models/Feedback.cs                   validation rules + custom attribute
