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
  sh(label: 'Pulling env files...', script: './scripts/env/pull-env.sh')
}

def ensureCerts() {
  sh(label: 'Ensuring that certificates are present...', script: "./scripts/cert/setup.sh")
}

def buildServices() {
  ensureEnv()
  ensureCerts()
  sh(label: 'Building services...', script: './scripts/local/build-all-services.sh')
  sh(label: 'Building docker images...', script: './scripts/docker/build-services.sh')
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
  ensureEnv()
  ensureCerts()
  ensureK8sCluster()
  sh(label: 'Running K8s deploy script...', script: './scripts/k8s/deploy.sh')

  def deployments = sh(
    script: 'kubectl get deployments -o name --no-headers',
    returnStdout: true
  ).trim().split('\n')

  for (d in deployments) {
    sh(label: "Checking rollout status for ${d}...", script: """
      kubectl rollout status ${d} --timeout=5m || {
        kubectl logs "${d}";
        exit 1;
      }
    """)
  }
}

pipeline {
  agent none

  environment {
    INFISICAL_TOKEN = credentials('infisical-token')
  }

  stages {
    stage('Test deployment') {
      agent { label 'deploy' }

      steps {
        script {
            setBuildStatus('Testing deployment...', 'PENDING', 'jenkins/ci/deployment')
            deploy()
        }
      }

      post {
        success {
          setBuildStatus('Deployment succeeded', 'SUCCESS', 'jenkins/ci/deployment')
        }

        failure {
          setBuildStatus('Deployment failed', 'FAILURE', 'jenkins/ci/deployment')
        }
      }
    }
  }
}
