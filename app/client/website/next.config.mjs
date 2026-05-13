import createNextIntlPlugin from "next-intl/plugin";
import 'dotenv/config';

/** @type {import('next').NextConfig} */
const nextConfig = {
  // update image remote later
  basePath: process.env.BASE_PATH,
  output: 'standalone',
  reactStrictMode: true,
  images: {
    qualities: [75, 100],
    remotePatterns: [
      {
        protocol: "https",
        hostname: "**"
      }
    ],
  },
  typescript: {
    ignoreBuildErrors: true
  }
};

const withNextIntl = createNextIntlPlugin();
export default withNextIntl(nextConfig);
