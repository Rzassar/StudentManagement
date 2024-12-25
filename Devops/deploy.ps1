# Apply manifests in order
kubectl apply -f k8s/sql-server.yaml
kubectl apply -f k8s/backend.yaml
kubectl apply -f k8s/frontend.yaml

# Wait for pods
kubectl wait --for=condition=ready pod -l app=mssql --timeout=60s
kubectl wait --for=condition=ready pod -l app=backend --timeout=60s
kubectl wait --for=condition=ready pod -l app=frontend --timeout=60s

# Display status
kubectl get pods
kubectl get services