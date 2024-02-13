import * as React from 'react';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import Link from '@mui/material/Link';

import { DataGridApp } from './DataGrid';

import {
  QueryClient,
  QueryClientProvider,
} from '@tanstack/react-query'

function Copyright() {
  return (
    <Typography variant="body2" color="text.secondary" align="center">
      {'Copyright © '}
      <Link color="inherit" href="https://mui.com/">
        Your Website
      </Link>{' '}
      {new Date().getFullYear()}.
    </Typography>
  );
}

export default function App() {

  const queryClient = new QueryClient()

  return (
    <QueryClientProvider client={queryClient}>
      <Container maxWidth="lg">
        <Box sx={{ my: 4 }}>
          <DataGridApp />
        </Box>
      </Container>
    </QueryClientProvider>
  );
}
