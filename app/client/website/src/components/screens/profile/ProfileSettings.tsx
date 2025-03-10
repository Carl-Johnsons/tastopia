// components/profile/ProfileSettings.jsx
import { ArrowDownIcon, LoadingIcon, SaveIcon } from "@/components/shared/icons";
import { useState } from "react";

export default function ProfileSettings() {
  const [theme, setTheme] = useState("Light");
  const [language, setLanguage] = useState("English");
  const [isSaving, setIsSaving] = useState(false);

  const handleSave = () => {
    setIsSaving(true);

    setTimeout(() => {
      setIsSaving(false);
    }, 1000);
  };

  return (
    <div className="bg-white_black100 rounded-xl border border-gray-200 p-6 shadow-sm dark:border-gray-600">
      <h2 className="h3-semibold text-black_white mb-6">Settings</h2>

      <div className="space-y-6">
        <div>
          <label className="mb-2 block text-sm font-medium text-gray-500">Theme</label>
          <div className="relative">
            <select
              value={theme}
              onChange={(e) => setTheme(e.target.value)}
              className="bg-white_black100 w-full appearance-none rounded-lg border border-gray-300 px-4 py-2 pr-8 text-gray-500 focus:border-blue-500 focus:outline-none
"
            >
              <option value="Light">Light</option>
              <option value="Dark">Dark</option>
              <option value="System">System</option>
            </select>
            <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-500">
              <ArrowDownIcon />
            </div>
          </div>
        </div>

        <div>
          <label className="mb-2 block text-sm font-medium text-gray-500">Language</label>
          <div className="relative">
            <select
              value={language}
              onChange={(e) => setLanguage(e.target.value)}
              className="bg-white_black100 w-full appearance-none rounded-lg border border-gray-300 px-4 py-2 pr-8 text-gray-500 focus:border-blue-500 focus:outline-none"
            >
              <option value="English">English</option>
              <option value="Vietnamese">Vietnamese</option>
            </select>
            <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-500">
              <ArrowDownIcon />
            </div>
          </div>
        </div>

        <button
          onClick={handleSave}
          className="flex w-full items-center justify-center gap-2 rounded-lg bg-orange-500 px-4 py-2 font-medium text-white transition hover:bg-orange-600"
          disabled={isSaving}
        >
          {isSaving ? (
            <>
              <LoadingIcon />
              Saving...
            </>
          ) : (
            <>
              <SaveIcon />
              Save Changes
            </>
          )}
        </button>
      </div>
    </div>
  );
}
