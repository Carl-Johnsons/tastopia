import createNextIntlPlugin from "next-intl/plugin";
import 'dotenv/config';

/** @type {import('next').NextConfig} */
const nextConfig = {
  // update image remote later
  basePath: process.env.BASE_PATH,
  output: 'standalone',
  reactStrictMode: true,
  images: {
    remotePatterns: [
      {
        protocol: "https",
        hostname: "res.cloudinary.com",
        pathname: "/**"
      }
    ],
  },
  typescript: {
    ignoreBuildErrors: true
  },
  eslint: {
    ignoreDuringBuilds: true
  }
};

const withNextIntl = createNextIntlPlugin();
export default withNextIntl(nextConfig);
