import createNextIntlPlugin from "next-intl/plugin";

/** @type {import('next').NextConfig} */
const nextConfig = {
  // update image remote later
  basePath: '/tastopia/ci/deploy/dashboard',
  output: 'standalone',
  reactStrictMode: true,
  images: {
    remotePatterns: [
      {
        protocol: "https",
        hostname: "**",
        pathname: "/**"
      }
    ],
    domains: ["res.cloudinary.com"]
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
