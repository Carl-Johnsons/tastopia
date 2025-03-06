import React from "react";

const Layout = ({ children }: { children: React.ReactNode }) => {
  return (
    <main className="background-light850_dark100 relative flex size-full flex-col items-center">
      <div className="flex-center mt-4 flex-col">
        <div className="w-full">{children}</div>
      </div>
    </main>
  );
};

export default Layout;
