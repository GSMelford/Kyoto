#!groovy

pipeline {
    agent any
    options {
        timestamps()
        disableConcurrentBuilds()
    }

    stages {
        stage("Build Kyoto Bot") {
            steps {
                echo "====== Building image... ======"
                sh "sudo docker build -t gsmelford/kyoto.bot.factory:dev ."
                echo "====== Build completed ======"
            }
        }
        stage("Build Kyoto Bot Telegram Sender") {
            steps {    
                echo "====== Building image... ======"
                sh "sudo docker build -f sender.Dockerfile -t gsmelford/kyoto.telegram.sender:dev ."
                echo "====== Build completed ======"
            }
        }
        stage("Build Kyoto Bot Telegram Receiver") {
            steps {    
                echo "====== Building image... ======"
                sh "sudo docker build -f receiver.Dockerfile -t gsmelford/kyoto.telegram.receiver:dev ."
                echo "====== Build completed ======"
            }
        }
        stage("Build Kyoto Bot Client") {
            steps {
                echo "====== Building image... ======"
                sh "sudo docker build -f client.Dockerfile -t gsmelford/kyoto.bot.client:dev ."
                echo "====== Build completed ======"
            }
        }
    }
}