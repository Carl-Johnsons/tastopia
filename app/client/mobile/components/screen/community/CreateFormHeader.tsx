import { Text, View } from "react-native";
import Input from "@/components/common/Input";
import { useTranslation } from "react-i18next";
import { Control, FieldErrors } from "react-hook-form";
import UploadImage from "@/components/common/UploadImage";

type HeaderFormFields = {
  title: string;
  description: string;
  serves: number;
  cookTime: string;
};

type FormHeaderProps = {
  images: ImageFileType[];
  onFileChange: (files: ImageFileType[]) => void;
  formControl: Control<HeaderFormFields, any>;
  errors: FieldErrors<HeaderFormFields>;
};

const CreateFormHeader = ({
  images,
  onFileChange,
  formControl,
  errors
}: FormHeaderProps) => {
  const { t } = useTranslation("createRecipe");

  return (
    <View className='mt-4 gap-4'>
      <View>
        <UploadImage
          defaultImages={images}
          onFileChange={onFileChange}
          isMultiple={false}
        />
      </View>
      <View>
        <Input
          variant='secondary'
          control={formControl}
          name='title'
          placeHolder={t("formPlaceholder.title")}
          errors={[t(errors.title?.message ?? "")]}
        />
      </View>
      <View>
        <Input
          variant='secondary'
          control={formControl}
          name='description'
          placeHolder={t("formPlaceholder.description")}
          errors={[t(errors.description?.message ?? "")]}
          multiline={true}
        />
      </View>
      <View>
        <Text className='body-semibold text-black_white'>{t("formTitle.serves")}</Text>
        <Input
          isNumeric={true}
          variant='secondary'
          control={formControl}
          name='serves'
          placeHolder={t("formPlaceholder.serves")}
          errors={[t(errors.serves?.message ?? "")]}
        />
      </View>

      <View>
        <Text className='body-semibold text-black_white'>{t("formTitle.cookTime")}</Text>
        <Input
          variant='secondary'
          control={formControl}
          name='cookTime'
          placeHolder={t("formPlaceholder.cookTime")}
          errors={[t(errors.cookTime?.message ?? "")]}
        />
      </View>
    </View>
  );
};

export default CreateFormHeader;
