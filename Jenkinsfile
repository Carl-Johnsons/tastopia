void setBuildStatus(String message, String state, String context) {
  step([
      $class: 'GitHubCommitStatusSetter',
      reposSource: [$class: 'ManuallyEnteredRepositorySource',
                          url: 'https://github.com/Carl-Johnsons/tastopia'],
      contextSource: [$class: 'ManuallyEnteredCommitContextSource', context: context],
      errorHandlers: [[$class: 'ChangingBuildStatusErrorHandler', result: 'UNSTABLE']],
      statusResultSource: [ $class: 'ConditionalStatusResultSource',
                          results: [[$class: 'AnyBuildResult', message: message, state: state]] ]
  ])
}

def getChangedPaths() {
  def prev = env.GIT_PREVIOUS_SUCCESSFUL_COMMIT
  if (!prev) {
    echo 'No previous successful commit found, full rebuild.'
    return null
  }

  echo "Last git previous successful commit: ${prev}"
  def output = sh(
        script: "git diff --name-only ${prev} HEAD",
        returnStdout: true
    ).trim()

  if (output == '') return []
  return output.tokenize('\n')
}

def detectServicesToBuild(changedFiles, services) {
  def buildList = []

  services.each { service, folders ->
    def affected = changedFiles.any { file ->
      folders.any { folder -> file.startsWith(folder) }
    }

    if (affected) {
        buildList << service
    }
  }
  return buildList
}

def ensureSecret() {
  sh(label: 'Ensuring that Infisical token is present...',
     script: "echo 'INFISICAL_TOKEN=${INFISICAL_TOKEN}' > .env.local")
  sh(label: 'Showing .env.local (sanitized)...', script: 'sed \'s/=.+/=******/\' < .env.local')
  sh(label: 'Pulling env files...', script: "./scripts/env/pull-env.sh ${params.DEPLOY_ENV}")

  withCredentials([file(credentialsId: 'gmail-access-token', variable: 'GMAIL_TOKEN_FILE')]) {
    sh(label: 'copy gmail-access-token to $WORKSPACE/data/Auth.Store', script: '''
      mkdir -p $WORKSPACE/data/Auth.Store
      cp $GMAIL_TOKEN_FILE $WORKSPACE/data/Auth.Store/Google.Apis.Auth.OAuth2.Responses.TokenResponse-user
      chmod 600 $WORKSPACE/data/Auth.Store/Google.Apis.Auth.OAuth2.Responses.TokenResponse-user
      ls -la $WORKSPACE/data/Auth.Store
    ''')
  }

  withCredentials([file(credentialsId: 'gmail-credentials', variable: 'GMAIL_CREDENTIAL_FILE')]) {
    sh(label: 'copy gmail-credentials to $WORKSPACE/app/server/NotificationService/src/EmailWorker', script: '''
      mkdir -p $WORKSPACE/app/server/NotificationService/src/EmailWorker
      cp $GMAIL_CREDENTIAL_FILE $WORKSPACE/app/server/NotificationService/src/EmailWorker/credentials.json
      chmod 600 $WORKSPACE/app/server/NotificationService/src/EmailWorker/credentials.json
      ls -la $WORKSPACE/app/server/NotificationService/src/EmailWorker | grep credentials
    ''')
  }
}

def cleanUp() {
  sh(label: 'Cleaning up ...', script: '''
    echo Clean up unused docker container...
    docker container prune -f
    echo Clean up unused docker image...
    docker image prune -a -f
    echo Clean up unused docker volume...
    docker volume prune -f
    echo Clean up unused docker network...
    docker network prune -f
    echo Clean up docker system...
    docker system prune -f
  ''')
}

def abortDueToEmptyBuildList() {
  currentBuild.result = 'ABORTED'
  setBuildStatus('Build check succeeded. There is nothing to build', 'SUCCESS', 'jenkins/ci/build')
  error('Stopping early due to empty service building list…')
}

def detectChange() {
  def serviceFolders = [
    'website': [ 'app/client/website' ],
    'api-gateway': [ 'app/server/APIGateway' ],
    'signalr': [ 'app/server/SignalRService' ],
    'tracking-api': [ 'app/server/TrackingService' ],
    'upload-api': [ 'app/server/UploadFileService' ],
    'identity-api': [ 'app/server/IdentityService' ],
    'notification-api': [
      'app/server/NotificationService/src/NotificationService.API',
      'app/server/NotificationService/src/NotificationService.Application',
      'app/server/NotificationService/src/NotificationService.Domain',
      'app/server/NotificationService/src/NotificationService.Infrastructure'
    ],
    'recipe-api': [
      'app/server/RecipeService/src/RecipeService.API',
      'app/server/RecipeService/src/RecipeService.Application',
      'app/server/RecipeService/src/RecipeService.Domain',
      'app/server/RecipeService/src/RecipeService.Infrastructure',
    ],
    'user-api': [ 'app/server/UserService' ],
  'ingredient-predict-api': [ 'app/server/IngredientPredictService' ],
  'email-worker': [ 'app/server/NotificationService/src/EmailWorker' ],
  'sms-worker': [ 'app/server/NotificationService/src/SMSWorker' ],
  'push-notification-worker': [ 'app/server/NotificationService/src/PushNotificationWorker' ],
  'recipe-worker': [ 'app/server/RecipeService/src/RecipeWorker' ]
  ]

  def validServices = serviceFolders.keySet() as List
  def manualList = []

  if (params.BUILD_SERVICES == 'default' ) {
    servicesToBuild = serviceFolders.keySet()
    echo "Manual override: building ${servicesToBuild.join(', ')}"
    return
  }

  if (params.BUILD_SERVICES) {
    manualList = params.BUILD_SERVICES
        .trim()
        .tokenize(',')
        .collect { it.trim() }
        .findAll { it }

    def invalid = manualList.findAll { !validServices.contains(it) }

    if (!invalid.isEmpty()) {
      error "Invalid service name(s): ${invalid}. Valid values: ${validServices}"
    }

    servicesToBuild = manualList

    if (servicesToBuild.isEmpty()) {
      echo 'List of services provided is empty. Nothing to build.'
      abortDueToEmptyBuildList()
    }

    echo "Manual override: building ${servicesToBuild.join(', ')}"
    return
  }

  def changed = getChangedPaths()
  def filtered = []
  if (changed == null) {
    filtered = serviceFolders.keySet() as List
  } else {
    filtered = detectServicesToBuild(changed, serviceFolders)
  }

  if (filtered.isEmpty()) {
    echo 'No changed services. Nothing to build.'
    abortDueToEmptyBuildList()
  }

  servicesToBuild = filtered
  echo "Building services ${servicesToBuild.join(', ')}"
}

def buildServices() {
  if (servicesToBuild.isEmpty()) {
    return
  }

  ensureSecret()
  sh(label: 'Publish share library...', script: '''
    dotnet publish --packages "$(pwd)/data/nuget" \
      -o ./app/server/Contract/Contract/Published \
      ./app/server/Contract/Contract
  ''')

  echo "Building ${servicesToBuild.join(', ')}"

  servicesToBuild.each { service ->
    try {
      sh(
        label: "Building ${service}...",
        script: "bash ./scripts/docker/build-services.sh -e ${params.DEPLOY_ENV} ${service}"
      )
      cleanUp()
    } catch (err) {
      sh(label: "Printing build logs for failed build...", script: 'cat build.log')
      throw err
    }
  }
}

def triggerDeployPipeline() {
  if (servicesToBuild.isEmpty()) {
    return
  }

  def DEPLOY_SERVICES = servicesToBuild.join(' ')

  if (params.BUILD_SERVICES == 'default') {
    DEPLOY_SERVICES = 'default'
  }
  
  build(
    job: '/tastopia/deployment-multibranch/master',
    parameters: [
      string(name: 'DEPLOY_SERVICES', value: DEPLOY_SERVICES),
      string(name: 'BUILD_BRANCH_NAME', value: env.BRANCH_NAME),
      string(name: 'DEPLOY_ENV', value: params.DEPLOY_ENV)
    ],
    wait: false
  )
}

pipeline {
  agent { label 'builder' }

  environment {
    INFISICAL_TOKEN = credentials('infisical-token')
  }

  parameters {
    string(
      name: 'BUILD_SERVICES',
      defaultValue: '',
      description: 'Comma-separated list of services to build (example: "consul,rabbitmq,mongo")'
    )

    string(
      name: 'DEPLOY_ENV',
      defaultValue: 'staging',
      description: 'The environment to trigger the deployment pipeline. Could be either "staging" or "production"'
    )

  }

  stages {
    stage('Detect service change') {
      steps {
        script {
          setBuildStatus('Begin detect change...', 'PENDING', 'jenkins/ci/build')
          detectChange()
        }
      }
      post {
        success {
          setBuildStatus('Detect service change succeeded', 'SUCCESS', 'jenkins/ci/build')
        }

        failure {
          setBuildStatus('Failed to detect changes', 'FAILURE', 'jenkins/ci/build')
        }
      }
    }
    stage('Build services') {
      steps {
        script {
          setBuildStatus('Building...', 'PENDING', 'jenkins/ci/build')
          buildServices()
        }
      }
      post {
        success {
          setBuildStatus('Build succeeded', 'SUCCESS', 'jenkins/ci/build')
        }

        failure {
          setBuildStatus('Build failed', 'FAILURE', 'jenkins/ci/build')
        }
      }
    }

    stage('Trigger Deploy Pipeline') {
      steps {
        script {
          triggerDeployPipeline()
        }
      }
    }
  }
}
