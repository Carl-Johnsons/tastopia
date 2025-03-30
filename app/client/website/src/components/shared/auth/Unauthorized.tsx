import LoginForm from "@/components/screen/login/LoginForm";
import Image from "../common/Image";

export default function Unauthorized() {
  return (
    <div className='grid h-screen sm:grid-cols-2'>
      <Image
        src='/assets/images/auth-figure.png'
        className='hidden sm:block'
        fill
        alt=''
      />
      <div className='flex-center flex-col gap-4 p-8'>
        <h1 className='text-3xl font-semibold'>Unauthorized</h1>
        <p className='text-lg text-gray-500'>
          You do not have permission to access this page.
        </p>
        <LoginForm />
      </div>
    </div>
  );
}
