import createNextIntlPlugin from "next-intl/plugin";

/** @type {import('next').NextConfig} */
const nextConfig = {
  // update image remote later
  reactStrictMode: true,
  images: {
    remotePatterns: [
      {
        protocol: "https",
        hostname: "**",
        pathname: "/**"
      }
    ]
  },
  typescript: {
    ignoreBuildErrors: true
  }
};

const withNextIntl = createNextIntlPlugin();
export default withNextIntl(nextConfig);
