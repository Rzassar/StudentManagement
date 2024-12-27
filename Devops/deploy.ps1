# Cleanup
kubectl delete -f k8s/frontend.yaml --ignore-not-found
kubectl delete -f k8s/backend.yaml --ignore-not-found
kubectl delete -f k8s/sql-server.yaml --ignore-not-found

# Wait for cleanup
Start-Sleep -Seconds 10

# Apply manifests
kubectl apply -f k8s/sql-server.yaml
kubectl wait --for=condition=ready pod -l app=mssql --timeout=60s

kubectl apply -f k8s/backend.yaml
kubectl wait --for=condition=ready pod -l app=backend --timeout=60s

kubectl apply -f k8s/frontend.yaml
kubectl wait --for=condition=ready pod -l app=frontend --timeout=60s

# Display status
kubectl get pods
kubectl get services