void setBuildStatus(String message, String state, String context) {
  step([
      $class: "GitHubCommitStatusSetter",
      reposSource: [$class: "ManuallyEnteredRepositorySource", url: "https://github.com/Carl-Johnsons/tastopia"],
      contextSource: [$class: "ManuallyEnteredCommitContextSource", context: context],
      errorHandlers: [[$class: "ChangingBuildStatusErrorHandler", result: "UNSTABLE"]],
      statusResultSource: [ $class: "ConditionalStatusResultSource", results: [[$class: "AnyBuildResult", message: message, state: state]] ]
  ]);
}

pipeline {
  agent none

  environment {
    INFISICAL_TOKEN = credentials('infisical-token')
  }

  stages {
    stage('Build') {
      agent { label 'server' }

      steps {
        setBuildStatus('Building...', "PENDING", "jenkins/ci/build")

        sh(script: """ whoami;pwd;ls -la """, label: "Checking info...")
        sh(label: "Create .env.local", script: 'echo "INFISICAL_TOKEN=${INFISICAL_TOKEN}" > .env.local')

        sh(label: "Show .env.local (sanitized)", script: '''cat .env.local''')
        // sh(label: "Setup-backend", script: ''' bash ./scripts/local/setup-backend.sh ''')

        setBuildStatus('Built successfully', "SUCCESS", "jenkins/ci/build")
      }
    }

    stage('Test deployment') {
      agent { label 'deploy' }

      steps {
        setBuildStatus('Testing deployment...', "PENDING", "jenkins/ci/deployment")
        sh(script: """ whoami;pwd;ls -la """, label: "Checking info...")
        setBuildStatus('Deployed successfully', "SUCCESS", "jenkins/ci/deployment")
      }
    }
  }
}
