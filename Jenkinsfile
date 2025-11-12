void setBuildStatus(String message, String state, String context) {
  step([
      $class: 'GitHubCommitStatusSetter',
      reposSource: [$class: 'ManuallyEnteredRepositorySource', url: 'https://github.com/Carl-Johnsons/tastopia'],
      contextSource: [$class: 'ManuallyEnteredCommitContextSource', context: context],
      errorHandlers: [[$class: 'ChangingBuildStatusErrorHandler', result: 'UNSTABLE']],
      statusResultSource: [ $class: 'ConditionalStatusResultSource', results: [[$class: 'AnyBuildResult', message: message, state: state]] ]
  ]);
}

def ensureEnv() {
  sh(label: 'Ensuring that Infisical token is present...', script: "echo 'INFISICAL_TOKEN=${INFISICAL_TOKEN}' > .env.local")
  sh(label: 'Showing .env.local (sanitized)...', script: 'sed \'s/=.*/=******/\' < .env.local')
}

def buildServices() {
  ensureEnv()
  sh(label: 'Building services...', script: './scripts/docker/build-services.sh')
}

def ensureK8sCluster() {
  sh(label: 'Ensuring that Kubernetes cluster is up...', script: '''
    if minikube status | grep -q Stopped; then
      echo Kubernetes cluster is not running
      echo Starting the cluster...
      minikube start
    fi

    if minikube status | grep -q Stopped; then
      echo 'ERROR: Kubernetes cluster failed to start'
      exit 1
    fi

    echo Kubernetes cluster is online
  ''')
}

def deploy() {
  ensureK8sCluster()
  sh(label: 'Running K8s deploy script...', script: '''
    path=./scripts/k8s/deploy.sh
    chmod 644 $path
    $path
  ''')
}

pipeline {
  agent none

  stages {
    stage('Build') {
      agent { label 'server' }

      environment {
        INFISICAL_TOKEN = credentials('infisical-token')
      }

      steps {
        script {
          try {
            setBuildStatus('Building...', 'PENDING', 'jenkins/ci/build')
            buildServices()
            setBuildStatus('Build succeeded', 'SUCCESS', 'jenkins/ci/build')
          } catch (Exception err) {
            setBuildStatus('Build failed', 'FAILURE', 'jenkins/ci/build')
            throw err
          }
        }
      }
    }

    stage('Test deployment') {
      agent { label 'deploy' }

      steps {
        script {
          try {
            setBuildStatus('Testing deployment...', 'PENDING', 'jenkins/ci/deployment')
            deploy()
            setBuildStatus('Deployment succeeded', 'SUCCESS', 'jenkins/ci/deployment')
          } catch (Exception err) {
            setBuildStatus('Deployment failed', 'FAILURE', 'jenkins/ci/deployment')
            throw err
          }
        }
      }
    }
  }
}
