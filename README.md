# Minimal .NET 6 Web API

Uses a [dev container](https://code.visualstudio.com/docs/remote/create-dev-container) approach to load the .NET 6 nightly SDK in a container for inner loop of development.

This is based on the [.NET 6 Preview 4](https://devblogs.microsoft.com/dotnet/announcing-net-6-preview-4/) blog post.

## Enabling Google APIs

If you have not already done so, make sure to enable the following APIs in your project.  You can do this with the following command, easiest if done in the Google Cloud shell:

```bash
gcloud services enable \
    containerregistry.googleapis.com \
    run.googleapis.com \
    compute.googleapis.com \
    cloudbuild.googleapis.com
```

## Set permissions for Cloud Build to deploy to Cloud Run

```bash
PROJECT_ID=`gcloud config list --format 'value(core.project)' 2>/dev/null`

PROJECT_NUMBER=`gcloud projects describe $PROJECT_ID --format='value(projectNumber)'`

gcloud projects add-iam-policy-binding $PROJECT_ID --member "serviceAccount:$PROJECT_NUMBER@cloudbuild.gserviceaccount.com" --role roles/run.admin

gcloud iam service-accounts add-iam-policy-binding $PROJECT_NUMBER-compute@developer.gserviceaccount.com --member "serviceAccount:$PROJECT_NUMBER@cloudbuild.gserviceaccount.com" --role "roles/iam.serviceAccountUser"    

gcloud projects add-iam-policy-binding $PROJECT_ID --condition=expression='resource.name.startsWith("projects/$PROJECT_NUMBER/secrets/connectionstrings")',title="Access connection string secret" --role=roles/secretmanager.secretAccessor --member=serviceAccount:$PROJECT_NUMBER-compute@developer.gserviceaccount.com
```