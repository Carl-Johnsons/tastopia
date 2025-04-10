import { Text, View } from "react-native";
import Input from "@/components/common/Input";
import { useTranslation } from "react-i18next";
import { Control, FieldErrors } from "react-hook-form";
import UpdateImage from "@/components/common/UpdateImage";
import { Dispatch, SetStateAction } from "react";
import { isOnlineImage } from "@/utils/file";
import { UpdateRecipeFormValue } from "@/schemas/update-recipe";

type FormHeaderProps = {
  images: UpdateImage;
  formControl: Control<UpdateRecipeFormValue, any>;
  errors: FieldErrors<UpdateRecipeFormValue>;
  setImages: Dispatch<SetStateAction<UpdateImage>>;
};

const UpdateFormHeader = ({
  images,
  formControl,
  errors,
  setImages
}: FormHeaderProps) => {
  const { t } = useTranslation("createRecipe");

  const onAddImage = (images: ImageFileType[]) => {
    setImages(prev => {
      return {
        ...prev,
        additionalImages: images
      };
    });
  };

  const onDeleteImage = (deleteImageUrl: string) => {
    if (isOnlineImage(deleteImageUrl)) {
      setImages(prev => {
        return {
          ...prev,
          deleteUrls: [...prev?.deleteUrls!, deleteImageUrl]
        };
      });
    } else {
      setImages(prev => {
        return {
          ...prev,
          additionalImages: prev?.additionalImages?.filter(
            image => image.uri !== deleteImageUrl
          )
        };
      });
    }
  };

  return (
    <View className='mt-4 gap-4'>
      <View>
        <UpdateImage
          onAddImage={onAddImage}
          onDeleteImage={onDeleteImage}
          isMultiple={false}
          images={images}
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

export default UpdateFormHeader;
