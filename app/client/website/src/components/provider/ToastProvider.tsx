import { useTheme } from '@/context/ThemeProvider';
import React from 'react'
import { Bounce, ToastContainer } from 'react-toastify'


const ToastProvider = () => {
  const { mode } = useTheme();
  return (
      <ToastContainer
        position="bottom-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick={false}
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme={mode}
        transition={Bounce}
    />
  )
}

export default ToastProvider