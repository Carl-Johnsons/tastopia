import Navbar from "@/components/shared/navbar/Navbar";
import LeftSidebar from "@/components/shared/sidebar/LeftSidebar";
import SignalR from "@/components/shared/common/SignalR";
import { ReactNode } from "react";

const Layout = ({ children }: { children: ReactNode }) => {
  return (
    <main className='background-light850_dark100 relative'>
      <SignalR />
      <Navbar />
      <div className='flex'>
        <LeftSidebar />
        <section className='bg-white_black200 mt-16 flex min-h-screen flex-1 flex-col px-2 pb-6 pt-10 max-md:pb-14 sm:px-4'>
          <div className='w-full max-w-full'>{children}</div>
        </section>
      </div>
    </main>
  );
};

export default Layout;
