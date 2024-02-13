import React, { useState } from 'react'
import { useForm } from 'react-hook-form';
import { GridRowsProp, } from '@mui/x-data-grid';
import { useQuery, useMutation } from '@tanstack/react-query';
import { Box, Button, Modal, Typography, TextField, TableContainer, Table, TableHead, TableRow, TableCell, TableBody, Paper } from '@mui/material';

const endPoint: string = 'http://localhost:5150/api/Products';

const ModalStyle = {
    position: 'absolute' as 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
    display: 'flex'
};

interface FormValues {
    id?: number
    name: string;
    description: string;
    price: number;
    stockQuantity: number;
}


export function DataGridApp() {
    const [open, setOpen] = useState(false);
    const { register, handleSubmit, reset, setValue, formState: { errors } } = useForm<FormValues>();
    const [rowModesModel, setRowModesModel] = React.useState<any>({});


    const { data, isLoading, refetch } = useQuery({
        queryKey: ['dataGrid'],
        queryFn: async () => {
            const response = await fetch(endPoint);
            if (!response.ok) return new Error
            return response.json();
        }
    });

    const { mutateAsync } = useMutation({
        mutationFn:
            (newProduct: FormValues) => {
                return fetch(endPoint, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(newProduct),
                });
            }
    });

    const { mutateAsync: updateRowMutation } = useMutation({
        mutationFn:
            (newProduct: FormValues) => {
                return fetch(endPoint, {
                    method: 'PUT', 
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(newProduct),
                });
            }
    })

    const onSubmit = async (data: FormValues) => {

        if (data.id) {
            const result = await updateRowMutation(data);
            if (result) {
                ResetForm()
            } else { alert('Error update product') }
        } else {
            const { id, ...rest } = data
            const result = await mutateAsync({ ...rest });
            if (result) {
                ResetForm()
            } else { alert('Error save product') }

        }
    };

    function ResetForm(){
        reset()
        refetch()
        setOpen(false)
    }

    const handelEdit = (Obj: any) => {
        setOpen(true)
        setValue('id', Obj.id)
        setValue('name', Obj.name)
        setValue('description', Obj.description)
        setValue('price', Obj.price)
        setValue('stockQuantity', Obj.stockQuantity)
    }


    if (isLoading) return <div>loading...</div>

    const generateColumns = (data: any[]) => {
        if (data.length === 0) return [];
        return Object.keys(data[0])
    };

    const columns = generateColumns(data);
    const rows: GridRowsProp = data;



    return (
        <Box>
            <Box display={'flex'} flex={1} px={1} mb={1} justifyContent={'flex-end'}>
                <Button variant="contained" onClick={() => { reset(); setOpen(true) }}>Add New</Button>
            </Box>

            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Id</TableCell>
                            <TableCell>Description</TableCell>
                            <TableCell>Price</TableCell>
                            <TableCell>Adjusted Price</TableCell>
                            <TableCell>Stock Quantity</TableCell>
                            <TableCell>actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rows.map((row) => (
                            <TableRow
                                key={row.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {row.id}
                                </TableCell>
                                <TableCell  >{row.name}</TableCell>
                                <TableCell  >{row.description}</TableCell>
                                <TableCell  >{row.price}</TableCell>
                                <TableCell  >{row.adjustedPrice || '--'}</TableCell>
                                <TableCell  >{row.stockQuantity}</TableCell>
                                <TableCell  > <Button variant='outlined' onClick={() => handelEdit(row)}>Edit</Button>  </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>


            <Modal
                open={open}
                onClose={() => setOpen(false)}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
            >
                <Box sx={ModalStyle} flexDirection={'column'} gap={2}>

                    <input type="text" {...register('id')} style={{ display: 'none' }}/>

                    <TextField
                        {...register('name', { required: true })}
                        label="Name"
                        variant="outlined"
                        error={!!errors.name}
                        helperText={errors.name ? 'Name is required' : ''}
                    />
                    <TextField
                        {...register('description', { required: true })}
                        label="Description"
                        variant="outlined"
                        error={!!errors.description}
                        helperText={errors.name ? 'Description is required' : ''}
                    />
                    <TextField
                        {...register('price', { required: true, valueAsNumber: true })}
                        label="Price"
                        variant="outlined"
                        type="number"
                        error={!!errors.price}
                        helperText={errors.price ? 'Price is required' : ''}
                    />
                    <TextField
                        {...register('stockQuantity', { required: true, valueAsNumber: true })}
                        label="Stock Quantity"
                        variant="outlined"
                        type="number"
                        error={!!errors.stockQuantity}
                        helperText={errors.stockQuantity ? 'Stock Quantity is required' : ''}
                    />

                    <Button onClick={handleSubmit(onSubmit)} variant='contained'>Submit</Button>
                 </Box>
            </Modal>
        </Box>
    )
}
