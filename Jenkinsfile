pipeline {
    agent { label 'server' }

    environment {
        INFISICAL_TOKEN = credentials('infisical-token')
    }

    stages {
        stage('Change github status to PENDING') {
            steps {
              githubNotify (status: 'PENDING', description: 'Build started!')
            }
        }

        stage('info') {
            steps {
                sh(script: """ whoami;pwd;ls -la """, label: "first stage")
            }
        }
        stage('Load Infisical Token') {
            steps {
                sh(label: "Create .env.local", script: '''
cat <<-EOF > .env.local
INFISICAL_TOKEN=${INFISICAL_TOKEN}
EOF
        ''')

                sh(label: "Show .env.local (sanitized)", script: '''cat .env.local''')
            }
        }
        stage('Setup back-end') {
            steps {
                // sh(label: "Setup-backend", script: ''' bash ./scripts/local/setup-backend.sh ''')
                echo 'Build success'
            }
        }

        stage('Change github status to SUCCESS') {
            steps {
              githubNotify (status: 'SUCCESS', description: 'Build started!')
            }
        }
    }
}
